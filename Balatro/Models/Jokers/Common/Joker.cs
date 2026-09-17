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
    public class Joker : IJoker
    {
        public string Description { get; }
        public int Price { get; set; }
        public Rarity Rarity { get; }
        public Modifier Modifier { get; set; }
        public Visibility ButtonVisibility { get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonVisibility)));
                }
            }
        } = Visibility.Collapsed;
        public string Name { get; }
        public BitmapImage Image { get; }

        public Joker()
        {
            Description = "+4 Mult";
            Rarity = Rarity.COMMON;
            Modifier = Modifier.BASE;
            Price = 2;
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Joker.png"));
            Name = "Joker";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddEffect(Player player)
        {
            player.Multiplier += 4;
        }

    }
}
