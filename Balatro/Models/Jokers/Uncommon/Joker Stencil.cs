using Balatro.Enums;
using Cards.Balatro;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Uncommon
{
    internal class Joker_Stencil : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Joker_Stencil(Modifier modifier = Modifier.BASE)
        {
            Description = "X1 Mult for each empty Joker slot";
            Rarity = Rarity.UNCOMMON;
            Modifier = modifier;
            Price = 8;
        }

        public void AddEffect(Player player)
        {
            player.Multiplier *= (player.MaxJokerCount - player.PassiveJokers.Count - player.ActiveJokers.Count);
        }
    }
}
