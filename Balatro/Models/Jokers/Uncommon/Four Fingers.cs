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
    public class Four_Fingers : IPassiveJoker
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

        public Four_Fingers()
        {
            Description = "All Flushes and Straights can be made with 4 cards";
            Rarity = Rarity.UNCOMMON;
            Modifier = Modifier.BASE;
            Price = 7;
            Name = "Four Fingers";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Four_Fingers.png"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddEffect(Player player)
        {
            player.HandHandler.NeededCards4FlushAndStraight = 4;
        }

        public void DeactivateEffect(Player p)
        {
            p.HandHandler.NeededCards4FlushAndStraight = 5;
        }
    }
}
