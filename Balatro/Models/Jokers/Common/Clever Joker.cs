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
    public class Clever_Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; set; }
        public Visibility ButtonVisibility { get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonVisibility)));
                }
            }
        } = Visibility.Collapsed;
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public BitmapImage Image { get; }
        public string Name { get; }

        public Clever_Joker()
        {
            Description = "+80 Chips if played hand contains a Two Pair";
            Rarity = Rarity.COMMON;
            Modifier = Modifier.BASE;
            Price = 4;
            Name = "Clever Joker";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Clever_Joker.png"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.TWO_PAIR)) player.Chips += 80;
        }
    }
}
