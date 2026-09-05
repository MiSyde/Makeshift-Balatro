using Balatro.Enums;
using Balatro.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Crafty_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public ImageSource Image { get; }
        public int MinAnte { get; }
        public string Name { get; }

        public Crafty_Joker(Modifier modifier = Modifier.BASE)
        {
            Description = "+80 Chips if played hand contains a Flush";
            Rarity = Rarity.COMMON;
            Modifier = modifier;
            Price = 4;
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Crafty_Joker.png"));
            Name = "Crafty Joker";
        }

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.FLUSH)) player.Chips += 80;
        }
    }
}
