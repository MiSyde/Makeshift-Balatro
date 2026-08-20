using Balatro.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Mystic_Summit : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Mystic_Summit(Modifier modifier = Modifier.BASE)
        {
            Description = "+15 Mult when 0 discards remaining";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
        }

        public void AddEffect(Player player)
        {
            if (player.Discards == 0) player.Multiplier += 15;
        }
    }
}
