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
        private List<Card> InsertionSort(IList<Card> selectedCards)
        {
            var sorted = new List<Card>(selectedCards);
            for (int i = 1; i < selectedCards.Count; ++i)
            {
                var current = sorted[i];
                int j = i-1;

                while (j >= 0 && sorted[j].Value > current.Value)
                {
                    sorted[j + 1] = sorted[j];
                    --j;
                }
                sorted[j+1] = current;
            }
            return sorted;
        }

        public Hand CalculateHand(IList<Card> selectedCards, IList<Hand> playedHands)
        {
            Hand highestHand = Hand.HIGH_CARD;
            playedHands.Add(Hand.HIGH_CARD);

            if (selectedCards.Count == 1) return highestHand;

            var sorted = InsertionSort(selectedCards);

            Card previousCard = sorted[0];
            int straight = 1;
            int flush = 1;
            int kind = 1;
            int pair = 0;
            List<int> pairValues = new();
            int four = 0;
            int three = 0;
            int five = 0;

            for (int i = 1; i < selectedCards.Count; ++i)
            {
                Card currentCard = sorted[i];

                if (currentCard.Value == previousCard.Value + 1) ++straight;
                if (currentCard.Value == previousCard.Value && !currentCard.IsFaceCard && !previousCard.IsFaceCard) 
                {
                    ++kind;
                    if (kind == 2)
                    {
                        pairValues.Add(currentCard.Value);
                        ++pair;
                    }
                    else if(kind == 3)
                    {
                        ++three;
                    } 
                    else if(kind == 4)
                    {
                        ++four;
                    } 
                    else if(kind == 5)
                    {
                        ++five;
                    }
                }
                else if(currentCard.Value == previousCard.Value && currentCard.IsFaceCard && previousCard.IsFaceCard)
                {
                    ++kind;
                    if (kind == 2)
                    {
                        pairValues.Add(currentCard.Value);
                        ++pair;
                    }
                    else if (kind == 3)
                    {
                        ++three;
                    }
                    else if (kind == 4)
                    {
                        ++four;
                    }
                    else if (kind == 5)
                    {
                        ++five;
                    }
                }
                else { kind = 1; }

                if (currentCard.SuitType == previousCard.SuitType) ++flush;

                previousCard = currentCard;
            }

            if (pair == 1)
            {
                highestHand = Hand.PAIR;
                playedHands.Add(Hand.PAIR);
            }
            else if(pair == 2)
            {
                highestHand = Hand.TWO_PAIR;
                playedHands.Add(Hand.PAIR);
                playedHands.Add(Hand.TWO_PAIR);
            }

            if (three == 1)
            { 
                highestHand = Hand.THREE_OF_A_KIND;
                playedHands.Add(Hand.THREE_OF_A_KIND);
            }

            if (straight == NeededCards4FlushAndStraight) 
            { 
                highestHand = Hand.STRAIGHT;
                playedHands.Add(Hand.STRAIGHT);
            }

            else if (flush == NeededCards4FlushAndStraight) 
            { 
                highestHand = Hand.FLUSH;
                playedHands.Add(Hand.FLUSH);
            }

            if (three == 1 && pair == 1 && pairValues.Count != 1) 
            { 
                highestHand = Hand.FULL_HOUSE;
                playedHands.Add(Hand.FULL_HOUSE);
            }
            else if (four == 4)
            {
                highestHand = Hand.FOUR_OF_A_KIND;
                playedHands.Add(Hand.FOUR_OF_A_KIND);
            }

            if (straight == NeededCards4FlushAndStraight && flush == NeededCards4FlushAndStraight) 
            { 
                highestHand = Hand.STRAIGHT_FLUSH; 
                playedHands.Add(Hand.STRAIGHT_FLUSH);

                bool ace = false, king = false, queen = false, jack = false, ten = false;

                foreach(Card c in selectedCards)
                {
                    if (c.IsFaceCard && c.FaceCardType == FaceCard.King) king = true;
                    if (c.IsFaceCard && c.FaceCardType == FaceCard.Queen) queen = true;
                    if (c.IsFaceCard && c.FaceCardType == FaceCard.Jack) jack = true;
                    if (c.IsFaceCard && c.FaceCardType == FaceCard.Ace) ace = true;
                    if (!c.IsFaceCard && c.Value == 10) ten = true;
                }

                if(ace && king && queen && jack && ten)
                {
                    highestHand = Hand.STRAIGHT_FLUSH;
                    playedHands.Add(Hand.ROYAL_FLUSH);
                }
            }

            if(five == 1)
            {
                highestHand = Hand.FIVE_OF_A_KIND;
                playedHands.Add(Hand.FIVE_OF_A_KIND);
            }
            else if(flush == 5 && three == 1 && pair == 1 && pairValues.Count != 1)
            {
                highestHand = Hand.FLUSH_HOUSE;
                playedHands.Add(Hand.FLUSH_HOUSE);
            }
            
            if(flush == 5 && five == 1)
            {
                highestHand = Hand.FLUSH_FIVE;
                playedHands.Add(Hand.FLUSH_FIVE);
            }

            return highestHand;
        }
    }
}
