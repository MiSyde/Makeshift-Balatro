using Balatro.Enums;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Banner : IJoker
    {
        public string Name { get; }
        public ImageSource Image { get; }
        public int MinAnte { get; }
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }

        public Banner(Modifier modifier = Modifier.BASE)
        {
            Name = "Banner";
            Description = "+30 Chips for each remaining discard";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
        }

        public void AddEffect(Player player)
        {
            player.Chips += player.Discards * 30;
        }
    }
}
