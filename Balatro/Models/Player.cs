using Balatro.Models;
using Balatro.Models.Jokers;
using Cards.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Balatro
{
    internal class Player : INotifyPropertyChanged
    {
        public int MaxJokerCount { get; }
        private int _multiplier;
        private int _chips;
        private Hand _highestHand;
        public Hand HighestHand 
        { 
            get => _highestHand; 
            set 
            { 
                if(value != _highestHand) 
                { 
                    _highestHand = value;
                    OnPropertyChanged();
                } 
            }
        }
        public List<Hand> PlayedHands { get; }
        public HandHandler HandHandler { get; }
        public ObservableCollection<IJoker> ActiveJokers { get; }
        public ObservableCollection<IJoker> PassiveJokers { get; }
        public ObservableCollection<ITarot> TarotCards { get; }
        private Deck deck;
        public ObservableCollection<Card> SelectedCards { get; }
        public int RemainingHands
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
        public int Money { get; }
        public int Discards 
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
        public int TotalChips 
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
        public int Multiplier 
        { 
            get => _multiplier; 
            set 
            { 
                if(_multiplier != value) 
                { 
                    _multiplier = value;
                    OnPropertyChanged();
                } 
            }
        }
        public int Chips 
        {
            get => _chips; 
            set 
            { 
                if(_chips != value) 
                { 
                    _chips = value;
                    OnPropertyChanged();
                } 
            }
        }
        public ObservableDictionary<Hand, HandData> HandData;
        public ObservableDictionary<Hand, int> HandLevels;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Player()
        {
            MaxJokerCount = 5;
            TotalChips = 0;
            Discards = 3;

            deck = new Deck();
            TarotCards = new ObservableCollection<ITarot>();
            SelectedCards = new ObservableCollection<Card>();
            HandHandler = new HandHandler(5);
            ActiveJokers = new ObservableCollection<IJoker>();
            PassiveJokers = new ObservableCollection<IJoker>();
            PlayedHands = new List<Hand>();
            HandData = new ObservableDictionary<Hand, HandData>(() => OnPropertyChanged(nameof(HandData)));
            HandLevels = new ObservableDictionary<Hand, int>(() => OnPropertyChanged(nameof(HandLevels)));

            setUpHands();

            SelectedCards.CollectionChanged += calculateHand;
        }

        public Card drawFromDeck() => deck.Cards.ElementAt(Random.Shared.Next(0, deck.Cards.Count - 1));
      
        private void calculateHand(object? sender, NotifyCollectionChangedEventArgs e)
        {
            PlayedHands.Clear();

            if (SelectedCards.Count == 0)
            {
                Chips = 0;
                Multiplier = 0;
                return;
            }
            
            HandHandler.calculateHand(SelectedCards, ref _highestHand, PlayedHands);

            HandData.TryGetValue(HighestHand, out HandData? handInfo);
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
            HandData[Hand.HIGH_CARD] = new HandData(5, 1);
            HandData[Hand.PAIR] = new HandData(10, 2);
            HandData[Hand.TWO_PAIR] = new HandData(20, 2);
            HandData[Hand.THREE_OF_A_KIND] = new HandData(30, 3);
            HandData[Hand.STRAIGHT] = new HandData(30, 4);
            HandData[Hand.FLUSH] = new HandData(35, 4);
            HandData[Hand.FULL_HOUSE] = new HandData(40, 4);
            HandData[Hand.FOUR_OF_A_KIND] = new HandData(60, 7);
            HandData[Hand.STRAIGHT_FLUSH] = new HandData(100, 8);
            HandData[Hand.ROYAL_FLUSH] = new HandData(100, 8);

            HandLevels[Hand.HIGH_CARD] = 1;
            HandLevels[Hand.PAIR] = 1;
            HandLevels[Hand.TWO_PAIR] = 1;
            HandLevels[Hand.THREE_OF_A_KIND] = 1;
            HandLevels[Hand.STRAIGHT] = 1;
            HandLevels[Hand.FLUSH] = 1;
            HandLevels[Hand.FULL_HOUSE] = 1;
            HandLevels[Hand.FOUR_OF_A_KIND] = 1;
            HandLevels[Hand.STRAIGHT_FLUSH] = 1;
            HandLevels[Hand.ROYAL_FLUSH] = 1;
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
