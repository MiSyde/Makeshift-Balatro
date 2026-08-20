using Balatro.Enums;
using Balatro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Devious_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Devious_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+100 Chips if played hand contains a Straight";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
        }

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.STRAIGHT)) player.Chips += 100;
        }
    }
}
