using Balatro.Enums;
using Balatro.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Devious_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public ImageSource Image { get; }
        public int MinAnte { get; }
        public string Name { get; }

        public Devious_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+100 Chips if played hand contains a Straight";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
            Name = "Devious Joker";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Devious_Joker.png"));
        }

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.STRAIGHT)) player.Chips += 100;
        }
    }
}
