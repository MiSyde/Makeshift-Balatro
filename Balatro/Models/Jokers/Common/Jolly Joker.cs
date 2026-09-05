using Balatro.Enums;
using Balatro.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Jolly_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public string Name { get; }
        public ImageSource Image { get; }
        public int MinAnte { get; }

        public Jolly_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+8 Mult if played hand contains a Pair";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 3;
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Jolly_Joker.png"));
            Name = "Jolly Joker";
        }

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.PAIR)) player.Multiplier += 8;
        }
    }
}
