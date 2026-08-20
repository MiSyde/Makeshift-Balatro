using Balatro.Models;
using Cards.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Balatro
{
    public class BalatroGame : INotifyPropertyChanged
    {
        public Player player { get; }
        private RelayCommand<Card> cardPressedCommand;
        private RelayCommand discardCommand;
        private RelayCommand confirmCommand;
        private ObservableCollection<Card> cards;

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

            player = new Player();
            cards = new ObservableCollection<Card>();
            Blinds = new Dictionary<int, string>();
            SetUpBlinds();

            cardPressedCommand = new RelayCommand<Card>((c) => CardPressed(c));
            discardCommand = new RelayCommand(DiscardedCards, CanAct);
            confirmCommand = new RelayCommand(ConfirmedCards, CanAct);
        }   


        private void CardPressed(Card c)
        {
            if (!player.SelectedCards.Remove(c)) player.SelectedCards.Add(c);
        }

        private void ConfirmedCards()
        {
            --player.RemainingHands;
            ++player.HandTimes[player.HighestHand];

            player.CalculateChips();

            if (player.TotalChips >= Threshold) NextRound();
            else if (player.TotalChips < Threshold && player.RemainingHands == 0) EndGame();
        }

        private void DiscardedCards()
        {
            foreach(Card c in player.SelectedCards)
            {
                cards.Remove(c);
            }

            for(int i = 0; i < player.SelectedCards.Count; ++i)
            {
                cards.Add(player.DrawFromDeck());
            }

            player.SelectedCards.Clear();
        }

        private bool CanAct() => player.SelectedCards.Count != 0;

        private async void NextRound()
        {
            await Task.Delay(250);
            GiveMoney();
            player.RemainingHands = 4;
            player.Discards = 3;
            player.SelectedCards.Clear();
            cards.Clear();
            ++Round;
            if (Round % 3 == 0) NextStage();
        }

        private async void NextStage()
        {
            ++Ante;
        }

        private async void EndGame()
        {
            await Task.Delay(250);
        }

        private void SetUpBlinds()
        {
            Blinds[1] = "Small Blind";
            Blinds[2] = "Big Blind";
            Blinds[0] = "Boss Blind"; // Round 3 mod 3 
        }

        private void GiveMoney()
        {
            int basePrize = Round % 3;
            if(basePrize == 0)
            {
                player.Money += 10;
                return;
            }
            player.Money += (basePrize + 2);
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
