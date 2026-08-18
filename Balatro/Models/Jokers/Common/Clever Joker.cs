using Balatro.Enums;
using Cards.Balatro;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    internal class Clever_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Clever_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+80 Chips if played hand contains a Two Pair";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
        }

        public void AddEffect(Player player)
        {
            if (player.playedHands.Contains(Hand.TWO_PAIR)) player.Chips += 80;
        }
    }
}
