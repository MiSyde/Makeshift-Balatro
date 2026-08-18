using Cards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cards.Balatro
{
    internal class HandHandler
    {
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

        public void calculateHand(IList<Card> selectedCards, ref Hand highestHand)
        {
            highestHand = Hand.HIGH_CARD;
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
                    else if (kind == 2 && highestKind == 2) highestHand = Hand.TWO_PAIR;
                }
                if (currentCard.SuitType == previousCard.SuitType) ++flush;

                previousCard = currentCard;
            }
            if (highestKind == 2 && highestHand != Hand.TWO_PAIR) highestHand = Hand.PAIR;
            if (highestKind == 3) highestHand = Hand.THREE_OF_A_KIND;
            else if (highestKind == 4) highestHand = Hand.FOUR_OF_A_KIND;

            if (straight == 5) highestHand = Hand.STRAIGHT;
            else if (flush == 5) highestHand = Hand.FLUSH;

            if (highestKind == 3 && kind == 2) highestHand = Hand.FULL_HOUSE;

            if (straight == 5 && flush == 5) highestHand = Hand.STRAIGHT_FLUSH;
        }
    }
}
