using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balatro.Models;
using Balatro.Models.Jokers;
using Cards.Models;

namespace Cards.Balatro
{
    internal class Player
    {
        public int MaxJokerCount { get; }
        private int _multiplier;
        private int _chips;
        private Hand _highestHand;
        public Hand HighestHand 
        { 
            get => _highestHand; 
            set { if(value != _highestHand) { _highestHand = value; } }
        }
        public List<Hand> PlayedHands { get; }
        public HandHandler HandHandler { get; }
        public List<IJoker> ActiveJokers { get; }
        public List<IJoker> PassiveJokers { get; }
        public List<ITarot> TarotCards { get; }
        private Deck deck;
        public ObservableCollection<Card> SelectedCards { get; }
        public int RemainingHands
        {
            get;
            set { if (value != field) { field = value; } }
        }
        public int Money { get; }
        public int Discards 
        {
            get;
            set { if (value != field) { field = value; } } 
        }
        public int TotalChips 
        { 
            get; 
            set { if (value != field) { field = value; } }
        }
        public int Multiplier 
        { 
            get => _multiplier; 
            set { if(_multiplier != value) { _multiplier = value; } }
        }
        public int Chips 
        {
            get => _chips; 
            set { if(_chips != value) { _chips = value; } }
        }
        public Dictionary<Hand, HandData> HandData;
        public Dictionary<Hand, int> HandLevels;

        public Player()
        {
            MaxJokerCount = 5;
            TotalChips = 0;
            Discards = 3;

            deck = new Deck();
            TarotCards = new List<ITarot>();
            SelectedCards = new ObservableCollection<Card>();
            HandHandler = new HandHandler(5);
            ActiveJokers = new List<IJoker>();
            PassiveJokers = new List<IJoker>();
            PlayedHands = new List<Hand>();
            HandData = new Dictionary<Hand, HandData>();
            HandLevels = new Dictionary<Hand, int>();

            setUpHands();

            SelectedCards.CollectionChanged += calculateHand;
        }

        public Card drawFromDeck() => deck.Cards.ElementAt(Random.Shared.Next(0, deck.Cards.Count - 1));
      
        private void calculateHand(object? sender, NotifyCollectionChangedEventArgs e)
        {
            PlayedHands.Clear();

            HandHandler.calculateHand(SelectedCards, ref _highestHand, PlayedHands);

            HandData.TryGetValue(HighestHand, out HandData handInfo);
            Chips = handInfo.Chips;
            Multiplier = handInfo.Multiplier;
        }

        public async void applyJokers()
        {
            foreach(IJoker joker in ActiveJokers)
            {
                await Task.Delay(150);
                joker.AddEffect(this);
            }
        }

        public void calculateChips()
        {
            TotalChips += _chips * _multiplier;
        }

        private void setUpHands()
        {
            HandData.Add(Hand.HIGH_CARD, new HandData(5, 1));
            HandData.Add(Hand.PAIR, new HandData(10, 2));
            HandData.Add(Hand.TWO_PAIR, new HandData(20, 2));
            HandData.Add(Hand.THREE_OF_A_KIND, new HandData(30, 3));
            HandData.Add(Hand.STRAIGHT, new HandData(30, 4));
            HandData.Add(Hand.FLUSH, new HandData(35, 4));
            HandData.Add(Hand.FULL_HOUSE, new HandData(40, 4));
            HandData.Add(Hand.FOUR_OF_A_KIND, new HandData(60, 7));
            HandData.Add(Hand.STRAIGHT_FLUSH, new HandData(100, 8));
            HandData.Add(Hand.ROYAL_FLUSH, new HandData(100, 8));


            HandLevels.Add(Hand.HIGH_CARD, 1);
            HandLevels.Add(Hand.PAIR, 1);
            HandLevels.Add(Hand.TWO_PAIR, 1);
            HandLevels.Add(Hand.THREE_OF_A_KIND, 1);
            HandLevels.Add(Hand.STRAIGHT, 1);
            HandLevels.Add(Hand.FLUSH, 1);
            HandLevels.Add(Hand.FULL_HOUSE, 1);
            HandLevels.Add(Hand.FOUR_OF_A_KIND, 1);
            HandLevels.Add(Hand.STRAIGHT_FLUSH, 1);
            HandLevels.Add(Hand.ROYAL_FLUSH, 1);
        }

    }
}
