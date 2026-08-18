using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Cards.Models;

namespace Cards.Balatro
{
    internal class Player
    {
        private ObservableCollection<Card> _selectedCards;
        private Hand highestHand;
        private HandHandler handHandler;
        private List<IJoker> jokers;
        private Deck deck;
        private int _remainingHands;
        private int _discards;
        private int _points;
        private int _handValue;
        private int _multiplier;
        public ObservableCollection<Card> SelectedCards { get => _selectedCards; }
        public int RemainingHands 
        { 
            get => RemainingHands; 
            set
            {
                if (value != _remainingHands)
                    _remainingHands = value;
            }

        }
        public int Discards 
        { 
            get => _discards; 
            set 
            {
                if (value != _discards)
                    _discards = value;
            } 
        }
        public int Points 
        { 
            get => _points; 
            set
            {
                if (value != _points)
                    _points = value;
            }
        }
        public int Multiplier { get => _multiplier; }
        public int HandValue { get => _handValue; }

        public Player()
        {
            Points = 0;
            Discards = 3;
            _selectedCards = new();
            handHandler = new();
            jokers = new();

            SelectedCards.CollectionChanged += calculateHand;
        }

        public Card drawFromDeck() => deck.Cards.ElementAt(Random.Shared.Next(0, deck.Cards.Count - 1));
      
        private void calculateHand(object? sender, NotifyCollectionChangedEventArgs e)
        {
            handHandler.calculateHand(SelectedCards, ref highestHand);
        }

        public void applyJokers()
        {
            foreach(IJoker joker in jokers)
            {
                joker.AddEffect(SelectedCards, ref _handValue, ref _multiplier, highestHand);
            }
        }

        public void calculatePoints()
        {
            Points += _handValue * _multiplier;
        }

    }
}
