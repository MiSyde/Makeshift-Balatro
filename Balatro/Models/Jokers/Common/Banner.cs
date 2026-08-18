using Balatro.Enums;
using Cards.Balatro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    internal class Banner : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Banner(Modifier modifier = Modifier.BASE)
        {
            Description = "+30 Chips for each remaining discard";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
        }

        public void AddEffect(Player player)
        {
            player.Chips += player.Discards * 30;
        }
    }
}
