using Balatro.Enums;
using Cards.Balatro;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    internal class Droll_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Droll_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+10 Mult if played hand contains a Flush";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
        }

        public void AddEffect(Player player)
        {
            if (player.playedHands.Contains(Hand.FLUSH)) player.Multiplier += 10;
        }
    }
}
