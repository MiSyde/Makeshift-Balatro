using Balatro.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers
{
    public interface IJoker : IEffect
    {
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
    }
}
