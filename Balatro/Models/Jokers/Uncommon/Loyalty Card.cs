using Balatro.Enums;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;

namespace Balatro.Models.Jokers.Uncommon
{
    public class Loyalty_Card : IJoker
    {
        public Visibility ButtonVisibility { get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonVisibility)));
                }
            }
        } = Visibility.Collapsed;
        public string Description { get; }
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public string Name { get; }
        public BitmapImage Image { get; }
        private int remainingHands;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Loyalty_Card()
        {
            Description = "X4 Mult every 6 hands played";
            Rarity = Rarity.UNCOMMON;
            Modifier = Modifier.BASE;
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
