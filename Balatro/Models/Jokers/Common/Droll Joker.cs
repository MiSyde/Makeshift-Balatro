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
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public ImageSource Image { get; }
        public int MinAnte { get; }
        public string Name { get; }

        public Droll_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+10 Mult if played hand contains a Flush";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
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
