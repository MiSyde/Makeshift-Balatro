using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers
{
    public interface IPassiveJoker : IJoker
    {
        public abstract void DeactivateEffect(Player p);
    }
}
