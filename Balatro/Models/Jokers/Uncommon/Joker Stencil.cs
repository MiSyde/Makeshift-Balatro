using Balatro.Enums;
using Balatro.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Balatro.Models.Jokers.Uncommon
{
    public class Joker_Stencil : IJoker
    {
        public string Description { get; }
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public string Name { get; }
        public ImageSource Image { get; }

        public Joker_Stencil()
        {
            Description = "X1 Mult for each empty Joker slot";
            Rarity = Rarity.UNCOMMON;
            Modifier = Modifier.BASE;
            Price = 8;
            Name = "Joker Stencil";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Joker_Stencil.png"));
        }

        public void AddEffect(Player player)
        {
            player.Multiplier *= (player.MaxJokerCount - player.Jokers.Count);
        }
    }
}
