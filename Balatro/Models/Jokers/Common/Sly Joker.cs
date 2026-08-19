using Balatro.Enums;
using Cards.Balatro;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    internal class Sly_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Sly_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+50 Chips if played hand contains a Pair";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 3;
        }

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.PAIR)) player.Chips += 50;
        }
    }
}
