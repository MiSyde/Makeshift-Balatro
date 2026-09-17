using Balatro.Enums;
using Balatro.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Zany_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public string Name { get; }
        public BitmapImage Image { get; }
        public Visibility ButtonVisibility { get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonVisibility)));
                }
            }
        } = Visibility.Collapsed;

        public Zany_Joker()
        {
            Description = "+12 Mult if played hand contains a Three of a Kind";
            Rarity = Rarity.COMMON;
            Modifier = Modifier.BASE;
            Price = 4;
            Name = "Zany Joker";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Zany_Joker.png"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.THREE_OF_A_KIND)) player.Multiplier += 12;
        }
    }
}
