using Balatro.Enums;
using Balatro.Models;
using Balatro.Util;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using WinRT.Interop;

namespace Balatro
{
    public class BalatroGame : INotifyPropertyChanged
    {
        private RelayCommand? SmallBCmd;
        private RelayCommand? BigBCmd;
        private RelayCommand? BossBCmd;
        public BossBlind BossBlind {
            get;
            set
            {
                if (value != field)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }
        public SolidColorBrush BlindColor 
        { 
            get;
            set
            {
                if(value != field)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }
        public Player Player { get; }
        public RelayCommand DiscardCommand { get; }
        public RelayCommand ConfirmCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Threshold 
        { 
            get; 
            set
            {
                if(value != field)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }
        public Dictionary<int, string> Blinds;
        public int Round 
        { 
            get;
            set
            {
                if (value != field)
                {
                    field = value;
                    OnPropertyChanged();
                    UpdateSelectionCommands();
                }
            }
        }
        public int Ante
        {
            get;
            set
            {
                if(value != field)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }
        private int baseScore;
        

        public BalatroGame()
        {
            baseScore = 300;
            Round = 1;
            Threshold = 300;
            BlindColor = new SolidColorBrush(Color.FromArgb(255, 0, 104, 173));

            Player = new Player();
            Player.SelectedCards.CollectionChanged += RefreshCommands;
            Blinds = new Dictionary<int, string>();
            SetUpBlinds();

            DiscardCommand = new RelayCommand(DiscardedCards, CanDiscard);
            ConfirmCommand = new RelayCommand(ConfirmedCards, CanConfirm);
            DealCards();
        }   

        public void CardPressed(Card c)
        {
            if (!Player.SelectedCards.Remove(c)) Player.SelectedCards.Add(c);
        }

        private void UpdateSelectionCommands()
        {
            switch(Round % 4)
            {
                case 1:
                    SmallBCmd?.NotifyCanExecuteChanged();
                    break;
                case 2:
                    BigBCmd?.NotifyCanExecuteChanged();
                    break;
                case 3:
                    BossBCmd?.NotifyCanExecuteChanged();
                    break;
            }
        }

        public void AssignBlindCommands(RelayCommand Small, RelayCommand Big, RelayCommand Boss)
        {
            SmallBCmd = Small;
            BigBCmd = Big;
            BossBCmd = Boss;
        }

        private void RefreshCommands(object? sender, NotifyCollectionChangedEventArgs e)
        {
            ConfirmCommand.NotifyCanExecuteChanged();
            DiscardCommand.NotifyCanExecuteChanged();
        }

        private void ConfirmedCards()
        {
            --Player.RemainingHands;
            ++Player.HandTimes[Player.HighestHand];

            Player.CalculateChips();

            foreach (Card c in Player.SelectedCards)
            {
                Player.Cards.Remove(c);
            }

            Player.SelectedCards.Clear();

            if (Player.TotalChips >= Threshold) NextRound();
            else if (Player.TotalChips < Threshold && Player.RemainingHands == 0) EndGame();
            else DealCards();
        }

        private void DiscardedCards()
        {
            foreach(Card c in Player.SelectedCards)
            {
                Player.Cards.Remove(c);
            }

            Player.SelectedCards.Clear();
            --Player.Discards;
            DealCards();
        }

        private bool CanAct() => Player.SelectedCards.Count != 0;
        private bool CanDiscard() => Player.Discards != 0 && CanAct();
        private bool CanConfirm() => Player.RemainingHands != 0 && CanAct();

        public async void NextRound()
        {
            await Task.Delay(250);
            ++Round;

            GiveMoney();

            Player.RemainingHands = 4;
            Player.Discards = 3;

            Player.SelectedCards.Clear();
            Player.Cards.Clear();

            DealCards();

            ChangeThreshold();

            if (Round % 4 == 0) { App.MainFrame.Navigate(typeof(ShopPage)); }
        }

        private void ChangeThreshold()
        {
            switch(Round % 4)
            {
                case 1: Threshold = baseScore;
                    return;
                case 2: Threshold = (int)(baseScore * 1.5);
                    return;
                case 3:
                    Threshold = BossBlindThreshold();
                    return;
                default:
                    ++Ante;
                    IncreaseBaseScore();
                    return;
            }
        }

        private void IncreaseBaseScore()
        {
            switch(Ante)
            {
                case -1:
                case 0:
                    baseScore = 100;
                    return;
                case 1:
                    baseScore = 300;
                    return;
                case 2:
                    baseScore = 800;
                    return;
                case 3:
                    baseScore = 2000;
                    return;
                case 4:
                    baseScore = 5000;
                    return;
                case 5:
                    baseScore = 10000;
                    return;
                case 6:
                    baseScore = 20000;
                    return;
                case 7:
                    baseScore = 35000;
                    return;
                case 8:
                    baseScore = 50000;
                    return;
            }
        }

        private async void EndGame()
        {
            await Task.Delay(250);

        }

        private int BossBlindThreshold()
        {
            return baseScore * 2;
        }

        private void SetUpBlinds()
        {
            Blinds[1] = "Small Blind";
            Blinds[2] = "Big Blind";
            Blinds[3] = "Boss Blind";
        }

        private void GiveMoney()
        {
            int basePrize = Round % 4;
            Player.Money += (basePrize + 2);

            Player.Money += Player.RemainingHands;
        }

        private async void DealCards()
        {
            await Task.Delay(150);
            while(Player.Cards.Count != Player.CurrentCardHoldingSize)
            {
                Player.Cards.Add(Player.DrawFromDeck());
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
