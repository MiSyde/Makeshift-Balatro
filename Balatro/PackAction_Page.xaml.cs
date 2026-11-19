using Balatro.Models;
using Balatro.Models.Decks;
using Balatro.Models.Tarots;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Balatro
{
    /// <summary>
    /// The page that shows when the player buys a Pack
    /// </summary>
    public sealed partial class PackAction_Page : Page
    {
        Visibility ShowCardsGrid;
        BalatroGame Game => App.CurrentGame;
        Frame MainFrame => App.MainFrame;
        ObservableCollection<Card> ModifyableCards = new ObservableCollection<Card>();
        int ChosenCards = 0;
        Random Random = new Random();
        ConsumablePack ConsumablePack;
        IConsumable? SelectedConsumableCard;
        RelayCommand UseCardCommand;
        public PackAction_Page()
        {
            InitializeComponent();
            UseCardCommand = new RelayCommand(UseCard, CanUseCard);
        }

        private void Init()
        {
            SelectedConsumableCard = null;
            Game.Player.SelectedCards.Clear();
            ModifyableCards.Clear();
            ChosenCards = 0;
        }

        private bool CanUseCard() => SelectedConsumableCard is not null && SelectedConsumableCard.CanUse(Game.Player);

        private void UseCard()
        {
            ++ChosenCards;
            SelectedConsumableCard!.AddEffect(Game.Player);
            MainFrame.Navigate(typeof(ShopPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Init();

            if(e.Parameter is ConsumablePack Pack)
            {
                if(Pack.PackType is Enums.PackType.ARCANA || Pack.PackType is Enums.PackType.CELESTIAL || 
                    Pack.PackType is Enums.PackType.SPECTRAL)
                {
                    ShowCardsGrid = Visibility.Visible;
                    FillCardList();
                    ConsumablePack = Pack;
                } else
                {
                    ShowCardsGrid = Visibility.Collapsed;
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            Game.Player.Deck!.Cards.AddRange(ModifyableCards);
            ModifyableCards.Clear();
            Game.Player.SelectedCards.Clear();
        }

        private void FillCardList()
        {
            List<Card> d = Game.Player.Deck!.Cards;
            for (int i = 0; i < 12; ++i)
            {
                Card c = d[Random.Next(0, d.Count - 1)];
                ModifyableCards.Add(c);
                d.Remove(c);
            }
        }

        private void PackItemClick(object sender, ItemClickEventArgs e)
        {
            if(sender is IConsumable Consumable)
            {
                if (Consumable == SelectedConsumableCard) SelectedConsumableCard = null;
                else SelectedConsumableCard = Consumable;
            }
        }

        private void CardClick(object sender, ItemClickEventArgs e)
        {
            if(sender is Card c)
            {
                if (Game.Player.SelectedCards.Remove(c)) { return; }
                Game.Player.SelectedCards.Add(c);
            }
        }
    }
}
