using Balatro.Enums;
using Balatro.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Droll_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public ImageSource Image { get; }
        public string Name { get; }

        public Droll_Joker()
        {
            Description = "+10 Mult if played hand contains a Flush";
            Rarity = Rarity.COMMON;
            Modifier = Modifier.BASE;
            Price = 4;
            Name = "Droll Joker";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Droll_Joker.png"));
        }

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.FLUSH)) player.Multiplier += 10;
        }
    }
}
