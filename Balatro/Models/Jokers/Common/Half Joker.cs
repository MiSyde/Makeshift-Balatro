using Balatro.Enums;
using Balatro.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Half_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public ImageSource Image { get; }
        public string Name { get; }

        public Half_Joker()
        {
            Description = "+20 Mult if played hand contains 3 or fewer cards";
            Rarity = Rarity.COMMON;
            Modifier = Modifier.BASE;
            Price = 5;
            Name = "Half Joker";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Half_Joker.png"));
        }

        public void AddEffect(Player player)
        {
            if (player.SelectedCards.Count <= 3) player.Multiplier += 20;
        }
    }
}
