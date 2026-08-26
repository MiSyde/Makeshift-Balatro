using Balatro.Enums;
using Balatro.Models;
using Balatro.Models.Jokers;
using Balatro.Models.Vouchers;
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

namespace Balatro
{
    public class Player : INotifyPropertyChanged
    {
        public int MaxConsumableCount
        {
            get;
            set
            {
                if (field != value)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }
        public int MaxJokerCount 
        { 
            get; 
            set
            {
                if(field != value)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<IJoker> Jokers { get; }
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
        public ObservableCollection<IEffect> Consumables { get; }
        private readonly Deck _deck;
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
        public int Money 
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
        public ObservableDictionary<Hand, HandData> HandData { get; }
        public ObservableDictionary<Hand, int> HandLevels { get; }
        public ObservableDictionary<Hand, int> HandTimes { get; }
        public ObservableCollection<Card> Cards { get; }
        public IList<Card> PlayedCards { get; }
        public ObservableCollection<IVoucher> Vouchers { get; }
        public int CurrentCardHoldingSize
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
        public event PropertyChangedEventHandler? PropertyChanged;

        public Player()
        {
            MaxJokerCount = 5;
            TotalChips = 0;
            Discards = 3;
            Money = 10;
            RemainingHands = 4;
            CurrentCardHoldingSize = 8;
            MaxConsumableCount = 2;

            Cards = new ObservableCollection<Card>();
            _deck = new Deck();
            Consumables = new ObservableCollection<IEffect>();
            SelectedCards = new ObservableCollection<Card>();
            HandHandler = new HandHandler(5);
            Jokers = new ObservableCollection<IJoker>();
            PlayedHands = new List<Hand>();
            HandData = new ObservableDictionary<Hand, HandData>(() => OnPropertyChanged(nameof(HandData)));
            HandLevels = new ObservableDictionary<Hand, int>(() => OnPropertyChanged(nameof(HandLevels)));
            HandTimes = new ObservableDictionary<Hand, int>(() => OnPropertyChanged(nameof(HandTimes)));
            PlayedCards = new List<Card>();
            Vouchers = new ObservableCollection<IVoucher>();

            SetUpHandDictionaries();
            SelectedCards.CollectionChanged += CalculateHand;
        }

        public void AddJoker(IJoker joker) => Jokers.Add(joker);

        public Card DrawFromDeck()
        {
            Card c = _deck.Cards[Random.Shared.Next(0, _deck.CurrentSize)];
            _deck.Remove(c);
            return c;
        }
      
        private void CalculateHand(object? sender, NotifyCollectionChangedEventArgs e)
        {
            PlayedHands.Clear();
            PlayedCards.Clear();

            if (SelectedCards.Count == 0)
            {
                Chips = 0;
                Multiplier = 0;
                return;
            }

            HighestHand = HandHandler.CalculateHand(SelectedCards, PlayedHands, PlayedCards);

            HandData.TryGetValue(HighestHand, out HandData? handInfo);
            Chips = handInfo.Chips;
            Multiplier = handInfo.Multiplier;
        }

        public async void ApplyJokers()
        {
            foreach(IJoker joker in Jokers)
            {
                await Task.Delay(150);
                ApplyModifier(joker);
                await Task.Delay(150);
                if (joker is IPassiveJoker) continue;
                joker.AddEffect(this);
            }
        }

        public void CalculateChips()
        {
            ApplyJokers();
            TotalChips += _chips * _multiplier;
        }

        private void ApplyModifier(IJoker joker)
        {

        }

        private void SetUpHandDictionaries()
        {
            HandData[Hand.FLUSH_FIVE] = new HandData(160, 16);
            HandData[Hand.FLUSH_HOUSE] = new HandData(140, 14);
            HandData[Hand.FIVE_OF_A_KIND] = new HandData(120, 12);
            HandData[Hand.ROYAL_FLUSH] = new HandData(100, 8);
            HandData[Hand.STRAIGHT_FLUSH] = new HandData(100, 8);
            HandData[Hand.FOUR_OF_A_KIND] = new HandData(60, 7);
            HandData[Hand.FULL_HOUSE] = new HandData(40, 4);
            HandData[Hand.FLUSH] = new HandData(35, 4);
            HandData[Hand.STRAIGHT] = new HandData(30, 4);
            HandData[Hand.THREE_OF_A_KIND] = new HandData(30, 3);
            HandData[Hand.TWO_PAIR] = new HandData(20, 2);
            HandData[Hand.PAIR] = new HandData(10, 2);
            HandData[Hand.HIGH_CARD] = new HandData(5, 1);

            HandLevels[Hand.FLUSH_FIVE] = 1;
            HandLevels[Hand.FLUSH_HOUSE] = 1;
            HandLevels[Hand.FIVE_OF_A_KIND] = 1;
            HandLevels[Hand.ROYAL_FLUSH] = 1;
            HandLevels[Hand.STRAIGHT_FLUSH] = 1;
            HandLevels[Hand.FOUR_OF_A_KIND] = 1;
            HandLevels[Hand.FULL_HOUSE] = 1;
            HandLevels[Hand.FLUSH] = 1;
            HandLevels[Hand.STRAIGHT] = 1;
            HandLevels[Hand.THREE_OF_A_KIND] = 1;
            HandLevels[Hand.TWO_PAIR] = 1;
            HandLevels[Hand.PAIR] = 1;
            HandLevels[Hand.HIGH_CARD] = 1;

            HandTimes[Hand.FLUSH_FIVE] = 0;
            HandTimes[Hand.FLUSH_HOUSE] = 0;
            HandTimes[Hand.FIVE_OF_A_KIND] = 0;
            HandTimes[Hand.ROYAL_FLUSH] = 0;
            HandTimes[Hand.STRAIGHT_FLUSH] = 0;
            HandTimes[Hand.FOUR_OF_A_KIND] = 0;
            HandTimes[Hand.FULL_HOUSE] = 0;
            HandTimes[Hand.FLUSH] = 0;
            HandTimes[Hand.STRAIGHT] = 0;
            HandTimes[Hand.THREE_OF_A_KIND] = 0;
            HandTimes[Hand.TWO_PAIR] = 0;
            HandTimes[Hand.PAIR] = 0;
            HandTimes[Hand.HIGH_CARD] = 0;
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
