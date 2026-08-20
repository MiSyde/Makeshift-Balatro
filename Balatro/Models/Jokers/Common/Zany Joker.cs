using Balatro.Enums;
using Balatro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Zany_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Zany_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+12 Mult if played hand contains a Three of a Kind";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
        }

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.THREE_OF_A_KIND)) player.Multiplier += 12;
        }
    }
}
