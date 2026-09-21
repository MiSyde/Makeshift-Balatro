using Balatro.Enums;
using Balatro.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Balatro.Models
{
    public class HandHandler
    {
        public int NeededCards4FlushAndStraight
        {
            get;
            set { if (field != value) { field = value; } }
        }

        public HandHandler(int cardcount)
        {
            NeededCards4FlushAndStraight = cardcount;
        }
        private List<Card> InsertionSort(ObservableCollection<Card> selectedCards)
        {
            var sorted = new List<Card>(selectedCards);
            for (int i = 1; i < selectedCards.Count; ++i)
            {
                var current = sorted[i];
                int j = i-1;

                while (j >= 0 && sorted[j].RankOrder > current.RankOrder)
                {
                    sorted[j + 1] = sorted[j];
                    --j;
                }
                sorted[j+1] = current;
            }
            return sorted;
        }

        public Hand CalculateHand(ObservableCollection<Card> selectedCards, List<Hand> playedHands, List<Card> playedCards)
        {
            Hand highestHand = Hand.HIGH_CARD;
            playedHands.Add(Hand.HIGH_CARD);

            if (selectedCards.Count == 1)
            {
                playedCards.Add(selectedCards[0]);
                return highestHand;
            }

            var sorted = InsertionSort(selectedCards);

            Card previousCard = sorted[0];

            var rankToFrequency = new Dictionary<int, List<Card>>();
            rankToFrequency[previousCard.RankOrder] = new List<Card>();
            rankToFrequency[previousCard.RankOrder].Add(previousCard);

            Card? highestSoloCard = sorted[0];
            List<Card>? pairCardsA = null;
            List<Card>? pairCardsB = null;
            List<Card>? tripleCards = null;
            List<Card>? fourCards = null;
            int pair = 0;
            int three = 0;
            int four = 0;
            int five = 0;

            int straight = 1;
            int flush = 1;
            List<Card> straightCards = new();
            List<Card> flushCards = new();
            
            for (int i = 1; i < selectedCards.Count; ++i)
            {
                //
                Card currentCard = sorted[i];
                if (!rankToFrequency.TryGetValue(currentCard.RankOrder, out var val))
                    rankToFrequency[currentCard.RankOrder] = val = new List<Card>();
                val.Add(currentCard);
                //
                if (currentCard.RankOrder == previousCard.RankOrder + 1) 
                { 
                    ++straight;
                    if (!straightCards.Contains(previousCard)) 
                        straightCards.Add(previousCard);

                    straightCards.Add(currentCard);
                }
                if(currentCard.SuitType == previousCard.SuitType)
                {
                    ++flush;
                    if (!flushCards.Contains(previousCard))
                        flushCards.Add(previousCard);

                    flushCards.Add(currentCard);
                }
                //
                previousCard = currentCard;
            }

            foreach(var kvp in rankToFrequency)
            {
                switch(kvp.Value.Count)
                {
                    case 1:
                        if (kvp.Value.First().Value > highestSoloCard!.Value) highestSoloCard = kvp.Value.First();
                        break;
                    case 2:
                        if (pairCardsA == null) pairCardsA = kvp.Value;
                        else pairCardsB = kvp.Value;
                        ++pair;
                        break;
                    case 3:
                        ++three;
                        tripleCards = kvp.Value;
                        break;
                    case 4:
                        ++four;
                        fourCards = kvp.Value;
                        break;
                    case 5:
                        ++five;
                        break;
                }
            }

            if (pair == 1)
            {
                highestHand = Hand.PAIR;
                playedHands.Add(Hand.PAIR);
                playedCards.AddRange(pairCardsA!);
            }
            
            if(pair == 2)
            {
                highestHand = Hand.TWO_PAIR;
                playedHands.Add(Hand.PAIR);
                playedHands.Add(Hand.TWO_PAIR);
                playedCards.AddRange(pairCardsB!);
            }

            if (three == 1)
            { 
                highestHand = Hand.THREE_OF_A_KIND;
                playedHands.Add(Hand.THREE_OF_A_KIND);
                playedCards.AddRange(tripleCards!);
            }

            if (straight >= NeededCards4FlushAndStraight) 
            { 
                highestHand = Hand.STRAIGHT;
                playedHands.Add(Hand.STRAIGHT);
                playedCards.AddRange(straightCards);
            }

            if (flush >= NeededCards4FlushAndStraight) 
            { 
                highestHand = Hand.FLUSH;
                playedHands.Add(Hand.FLUSH);
                playedCards.AddRange(flushCards);
            }

            if (three == 1 && pair >= 1) 
            { 
                highestHand = Hand.FULL_HOUSE;
                playedHands.Add(Hand.FULL_HOUSE);
                playedCards = selectedCards.ToList();
            }
            
            if (four == 4)
            {
                highestHand = Hand.FOUR_OF_A_KIND;
                playedHands.Add(Hand.FOUR_OF_A_KIND);
                playedCards.AddRange(fourCards!);
            }

            if (straight >= NeededCards4FlushAndStraight && flush >= NeededCards4FlushAndStraight) 
            { 
                highestHand = Hand.STRAIGHT_FLUSH; 
                playedHands.Add(Hand.STRAIGHT_FLUSH);
                playedCards = straightCards.Count > flushCards.Count ? straightCards : flushCards;

                var royalGroup = sorted.Where(c => c.RankOrder >= 10 && c.RankOrder <= 14).ToList();
                if(royalGroup.Count >= NeededCards4FlushAndStraight)
                {
                    var royal = true;
                    Card previous = royalGroup[0];
                    for(int i = 1; i < royalGroup.Count; ++i)
                    {
                        Card current = royalGroup[i];
                        if (previous.RankOrder + 1 == current.RankOrder) continue;
                        royal = false;
                    }

                    if (royal)
                    {
                        highestHand = Hand.ROYAL_FLUSH;
                        playedHands.Add(Hand.ROYAL_FLUSH);
                    }
                }
                

            }

            if(five == 1)
            {
                highestHand = Hand.FIVE_OF_A_KIND;
                playedHands.Add(Hand.FIVE_OF_A_KIND);
                playedCards = selectedCards.ToList();
            }
            if(flush >= NeededCards4FlushAndStraight && three == 1 && pair == 1)
            {
                highestHand = Hand.FLUSH_HOUSE;
                playedHands.Add(Hand.FLUSH_HOUSE);
                playedCards = selectedCards.ToList();
            }
            
            if(flush >= NeededCards4FlushAndStraight && five == 1)
            {
                highestHand = Hand.FLUSH_FIVE;
                playedHands.Add(Hand.FLUSH_FIVE);
                playedCards = selectedCards.ToList(); ;
            }

            if(highestHand == Hand.HIGH_CARD)
            {
                playedCards.Add(highestSoloCard);
            }

            return highestHand;
        }
    }
}
