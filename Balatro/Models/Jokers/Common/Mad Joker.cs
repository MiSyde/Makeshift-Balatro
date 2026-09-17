using Balatro.Enums;
using Balatro.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;

namespace Balatro.Models.Jokers.Common
{
    public class Mad_Joker : IJoker
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

        public Mad_Joker()
        {
            Description = "+10 Mult if played hand contains a Two Pair";
            Rarity = Rarity.COMMON;
            Modifier = Modifier.BASE;
            Price = 4;
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Mad_Joker.png"));
            Name = "Mad Joker";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddEffect(Player player)
        {
            if (player.PlayedHands.Contains(Hand.TWO_PAIR)) player.Multiplier += 10;
        }
    }
}
