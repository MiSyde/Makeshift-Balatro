using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Planets
{
    public class Pluto : IEffect
    {
        public string Name => "Pluto";

        public string Description => "Increases High Card hand value by +1 Mult and +10 Chips";

        public ImageSource Image => new BitmapImage(new Uri("ms-appx:///Assets/PlanetImages/Pluto.png"));

        public void AddEffect(Player Player)
        {
            ++Player.HandLevels[Enums.Hand.HIGH_CARD];
            HandData Data = Player.HandData[Enums.Hand.HIGH_CARD];
            Player.HandData[Enums.Hand.HIGH_CARD] = new HandData(Data.Chips + 10, Data.Multiplier+1);
        }
    }
}
