using Balatro.Models;
using Cards.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Balatro
{
    internal class BalatroGame
    {
        public Player player { get; }
        private RelayCommand<Card> cardPressedCommand;
        private RelayCommand discardCommand;
        private RelayCommand confirmCommand;
        private List<Card> cards;
        public int threshold { get; }
        

        public BalatroGame()
        {
            threshold = 300;

            player = new();
            cards = new();

            cardPressedCommand = new RelayCommand<Card>((c) => CardPressed(c));
            discardCommand = new RelayCommand(DiscardedCards, CanAct);
            confirmCommand = new RelayCommand(ConfirmedCards, CanAct);
        }   


        private void CardPressed(Card c)
        {
            if (player.SelectedCards.Contains(c)) player.SelectedCards.Remove(c);
            else player.SelectedCards.Add(c);
        }

        private void ConfirmedCards()
        {
            --player.RemainingHands;

            player.calculateChips();

            if (player.TotalChips >= threshold) nextRound();
            else if (player.TotalChips < threshold && player.RemainingHands == 0) endGame();
        }

        private void DiscardedCards()
        {
            foreach(Card c in player.SelectedCards)
            {
                cards.Remove(c);
            }

            for(int i = 0; i < player.SelectedCards.Count; ++i)
            {
                cards.Add(player.drawFromDeck());
            }

            player.SelectedCards.Clear();
        }

        private bool CanAct() => player.SelectedCards.Count != 0;

        private async void nextRound()
        {
            await Task.Delay(250);
            player.Discards = 3;
            player.SelectedCards.Clear();
            cards.Clear();
        }

        private async void endGame()
        {
            await Task.Delay(250);
        }

    }
}
