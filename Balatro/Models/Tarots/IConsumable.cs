using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tarots
{
    public interface IConsumable : IEffect
    {
        public abstract bool CanUse(Player Player);
    }
}
