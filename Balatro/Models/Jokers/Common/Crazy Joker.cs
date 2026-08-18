using Balatro.Enums;
using Cards.Balatro;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    internal class Crazy_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Crazy_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "Gives +12 mult if played hand contains a Straight";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
        }
        public void AddEffect(Player player)
        {
            if (player.playedHands.Contains(Hand.STRAIGHT)) player.Multiplier += 12;
        }
    }
}
