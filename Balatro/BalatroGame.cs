using Balatro.Models;
using Balatro.Util;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WinRT.Interop;

namespace Balatro
{
    public class BalatroGame : INotifyPropertyChanged
    {
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
        

        public BalatroGame()
        {
            Threshold = 300;
            Round = 1;

            Player = new Player();
            Blinds = new Dictionary<int, string>();
            SetUpBlinds();

            DiscardCommand = new RelayCommand(DiscardedCards, CanAct);
            ConfirmCommand = new RelayCommand(ConfirmedCards, CanAct);
            DealCards();
        }   


        public void CardPressed(Card c)
        {
            if (!Player.SelectedCards.Remove(c)) Player.SelectedCards.Add(c);
        }

        private void ConfirmedCards()
        {

            Player.SelectedCards.Clear();
            --Player.RemainingHands;
            ++Player.HandTimes[Player.HighestHand];

            Player.CalculateChips();

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
            
            DealCards();
        }

        private bool CanAct() => Player.SelectedCards.Count != 0;

        private async void NextRound()
        {
            await Task.Delay(250);
            GiveMoney();
            Player.RemainingHands = 4;
            Player.Discards = 3;
            Player.SelectedCards.Clear();
            Player.Cards.Clear();
            ++Round;
            if (Round % 4 == 0) App.MainFrame.Navigate(typeof(ShopPage));
        }

        private async void EndGame()
        {
            await Task.Delay(250);

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
