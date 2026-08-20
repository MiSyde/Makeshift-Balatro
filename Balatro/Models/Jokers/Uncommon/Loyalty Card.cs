using Balatro.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Uncommon
{
    public class Loyalty_Card : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }
        private int remainingHands;

        public Loyalty_Card(Modifier modifier = Modifier.BASE)
        {
            Description = "X4 Mult every 6 hands played";
            Rarity = Rarity.UNCOMMON;
            Modifier = modifier;
            Price = 5;
            remainingHands = 6;
        }

        public void AddEffect(Player player)
        {
            if(remainingHands == 0)
            {
                player.Multiplier *= 4;
                remainingHands = 6;
            } 
            else
            {
                --remainingHands;
            }
        }
    }
}
