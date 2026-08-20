using Balatro.Enums;
using Cards.Balatro;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+4 Mult";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 2;
        }

        public void AddEffect(Player player)
        {
            player.Multiplier += 4;
        }

    }
}
