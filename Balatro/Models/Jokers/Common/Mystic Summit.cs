using Balatro.Enums;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Balatro.Models.Jokers.Common
{
    public class Mystic_Summit : IJoker
    {
        public string Description { get; }
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public string Name { get; }
        public Modifier Modifier { get; set; }
        public ImageSource Image { get; }

        public Mystic_Summit()
        {
            Description = "+15 Mult when 0 discards remaining";
            Rarity = Rarity.COMMON;
            Modifier = Modifier.BASE;
            Price = 4;
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Mystic_Summit.png"));
            Name = "Mystic Summit";
        }

        public void AddEffect(Player player)
        {
            if (player.Discards == 0) player.Multiplier += 15;
        }
    }
}
