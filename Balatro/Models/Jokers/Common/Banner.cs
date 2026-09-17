using Balatro.Enums;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models.Jokers.Common
{
    public class Banner : IJoker
    {
        public string Name { get; }
        public Visibility ButtonVisibility { get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonVisibility)));
                }
            }
        } = Visibility.Collapsed;
        public BitmapImage Image { get; }
        public string Description { get; }
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }

        public Banner()
        {
            Name = "Banner";
            Description = "+30 Chips for each remaining discard";
            Rarity = Rarity.COMMON;
            Modifier = Modifier.BASE;
            Price = 4;
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Banner.png"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddEffect(Player player)
        {
            player.Chips += player.Discards * 30;
        }
    }
}
