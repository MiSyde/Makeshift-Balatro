using Cards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Balatro
{
    internal interface IJoker
    {
        public abstract void AddEffect(IList<Card> cards, ref int points, ref int multiplier, Hand handType);
    }
}
