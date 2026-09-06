using Balatro.Enums;
using Balatro.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Sly_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public string Name { get; }
        public ImageSource Image { get; }

        public Sly_Joker()
        {
            Description = "+50 Chips if played hand contains a Pair";
            Rarity = Rarity.COMMON;
            Modifier = Modifier.BASE;
            Price = 3;
            Name = "Sly Joker";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Sly_Joker.png"));
        }

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.PAIR)) player.Chips += 50;
        }
    }
}
