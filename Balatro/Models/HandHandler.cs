using Cards.Balatro;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balatro.Models
{
    internal class HandHandler
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
        private void insertionSort(IList<Card> list)
        {
            if (list.Count == 1) return;

            for (int i = 0; i < list.Count; ++i)
            {
                int j = i + 1;
                while (j >= 0)
                {
                    Card atJ = list.ElementAt(j);
                    Card atI = list.ElementAt(i);
                    if (atJ.Value < atI.Value)
                    {
                        Card temp = atI;
                        atI = atJ;
                        atJ = temp;
                    }
                    --j;
                }
            }
        }

        public void calculateHand(IList<Card> selectedCards, ref Hand highestHand, IList<Hand> playedHands)
        {
            highestHand = Hand.HIGH_CARD;
            playedHands.Add(Hand.HIGH_CARD);

            if (selectedCards.Count == 1) return;

            insertionSort(selectedCards);

            Card previousCard = selectedCards.ElementAt(0);
            int straight = 1;
            int flush = 1;
            int kind = 1;
            int highestKind = 1;

            for (int i = 1; i < selectedCards.Count - 1; ++i)
            {
                Card currentCard = selectedCards.ElementAt(i);

                if (currentCard.Value == previousCard.Value + 1) ++straight;
                if (currentCard.Value == previousCard.Value) ++kind;
                else
                {
                    if (kind > highestKind)
                    {
                        highestKind = kind;
                        kind = 1;
                    }
                    else if (kind == 2 && highestKind == 2) 
                    { 
                        highestHand = Hand.TWO_PAIR;
                        playedHands.Add(Hand.TWO_PAIR);
                        playedHands.Add(Hand.PAIR);
                    }
                }
                if (currentCard.SuitType == previousCard.SuitType) ++flush;

                previousCard = currentCard;
            }

            if (highestKind == 2 && highestHand != Hand.TWO_PAIR)
            {
                highestHand = Hand.PAIR;
                playedHands.Add(Hand.PAIR);
            }
            else if (highestKind == 3)
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

            if (highestKind == 3 && kind == 2) 
            { 
                highestHand = Hand.FULL_HOUSE;
                playedHands.Add(Hand.FULL_HOUSE);
            }
            else if (highestKind == 4)
            {
                highestHand = Hand.FOUR_OF_A_KIND;
                playedHands.Add(Hand.FOUR_OF_A_KIND);
            }

            if (straight == NeededCards4FlushAndStraight && flush == NeededCards4FlushAndStraight) 
            { 
                highestHand = Hand.STRAIGHT_FLUSH; 
                playedHands.Add(Hand.STRAIGHT_FLUSH);
            }
            // royal flush
        }
    }
}
