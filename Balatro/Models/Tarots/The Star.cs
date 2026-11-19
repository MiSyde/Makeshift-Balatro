using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Balatro.Models.Tarots
{
    public class The_Star : IConsumable
    {
        public string Name => "The Star";

        public string Description => "Converts up to 3 selected cards to Diamonds";

        public BitmapImage Image => new(new Uri("ms-appx:///Assets/TarotImages/The_Star.png"));

        public Visibility ButtonVisibility
        {
            get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonVisibility)));
                }
            }
        } = Visibility.Collapsed;
        public int Price { get; set; } = 3;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddEffect(Player Player)
        {
            foreach(Card c in Player.SelectedCards.ToList())
            {
                c.Image = new(new Uri($"ms-appx:///Assets/CardImages/Diamonds/{c.RankOrder}.png"));
                c.SetDescription();
            }
        }

        public bool CanUse(Player Player) => Player.SelectedCards.Count <= 3 && Player.SelectedCards.Count > 0;
    }
}
