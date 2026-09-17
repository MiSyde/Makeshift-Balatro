using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models.Planets
{
    public class Pluto : IEffect
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
        public string Name => "Pluto";
        public int Price { get; set; } = 3;

        public string Description => "Increases High Card hand value by +1 Mult and +10 Chips";

        public BitmapImage Image => new(new Uri("ms-appx:///Assets/PlanetImages/Pluto.png"));

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddEffect(Player Player)
        {
            ++Player.HandLevels[Enums.Hand.HIGH_CARD];
            HandData Data = Player.HandData[Enums.Hand.HIGH_CARD];
            Player.HandData[Enums.Hand.HIGH_CARD] = new HandData(Data.Chips + 10, Data.Multiplier+1);
        }
    }
}
