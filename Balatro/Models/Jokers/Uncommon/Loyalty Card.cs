using Balatro.Enums;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Balatro.Models.Jokers.Uncommon
{
    public class Loyalty_Card : IJoker
    {
        public string Description { get; }
        public int Price { get; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public string Name { get; }
        public ImageSource Image { get; }
        public int MinAnte { get; }
        private int remainingHands;

        public Loyalty_Card(Modifier modifier = Modifier.BASE)
        {
            Description = "X4 Mult every 6 hands played";
            Rarity = Rarity.UNCOMMON;
            Modifier = modifier;
            Price = 5;
            remainingHands = 6;
            Name = "Loyalty Card";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Loyalty_Card.png"));
        }

        public void AddEffect(Player player)
        {
            if(remainingHands == 0)
            {
                player.Multiplier *= 4;
                remainingHands = 6;
            } 
            else
            {
                --remainingHands;
            }
        }
    }
}
