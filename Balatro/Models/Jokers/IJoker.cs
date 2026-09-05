using Balatro.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers
{
    public interface IJoker : IEffect
    {
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public int MinAnte { get; }
    }
}
