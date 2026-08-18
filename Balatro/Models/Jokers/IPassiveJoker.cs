using Cards.Balatro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers
{
    internal interface IPassiveJoker
    {
        public abstract void DeactivateEffect(Player p);
    }
}
