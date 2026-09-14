using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tarots
{
    public class The_Chariot : IConsumable
    {
        public string Name => "The Chariot";

        public string Description => "Enhances 1 selected card into a Steel Card";

        public BitmapImage Image => new(new Uri("ms-appx:///Assets/TarotImages/The_Chariot.png"));

        public void AddEffect(Player Player)
        {
            Player.SelectedCards[0].BaseEnhancement = Enhancement.STEEL_CARD;
        }

        public bool CanUse(Player Player) => Player.SelectedCards.Count == 1;
    }
}
