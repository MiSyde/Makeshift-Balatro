using Balatro.Enums;
using Cards.Balatro;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    internal class Jolly_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Jolly_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+8 Mult if played hand contains a Pair";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 3;
        }

        public void AddEffect(Player player)
        {
            if (player.playedHands.Contains(Hand.PAIR)) player.Multiplier += 8;
        }
    }
}
