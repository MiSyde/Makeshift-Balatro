using Balatro.Enums;
using Cards.Balatro;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    internal class Wily_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Wily_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+100 Chips if played hand contains a Three of a Kind";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
        }

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.THREE_OF_A_KIND)) player.Chips += 100;
        }
    }
}
