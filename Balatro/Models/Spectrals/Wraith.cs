using Balatro.Models.Tags;
using Balatro.Models.Tarots;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models.Spectrals
{
    public class Wraith : IConsumable
    {
        public string Name => "Wraith";

        public string Description => "Creates a random Rare Joker, sets money to $0";

        public BitmapImage Image => new(new Uri("ms-appx:///Assets/SpectralImages/Wraith.png"));

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

        public int Price
        {
            get => 10;
            set
            {
                field = 10;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddEffect(Player Player)
        {
            Player.Money = 0;
            Player.AddJoker(App.Shop.RareJokers[Random.Shared.Next(0, App.Shop.RareJokers.Count - 1)]);
        }

        public bool CanUse(Player Player)
        {
            return Player.Jokers.Count < Player.MaxJokerCount;
        }
    }
}
