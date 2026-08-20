using Balatro.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Uncommon
{
    public class Four_Fingers : IJoker, IPassiveJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; }

        public Four_Fingers(Modifier modifier = Modifier.BASE)
        {
            Description = "All Flushes and Straights can be made with 4 cards";
            Rarity = Rarity.UNCOMMON;
            Modifier = modifier;
            Price = 7;
        }

        public void AddEffect(Player player)
        {
            player.HandHandler.NeededCards4FlushAndStraight = 4;
        }

        public void DeactivateEffect(Player p)
        {
            p.HandHandler.NeededCards4FlushAndStraight = 5;
        }
    }
}
