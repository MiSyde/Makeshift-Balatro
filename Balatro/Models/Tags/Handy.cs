using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tags
{
    public class Handy : ITag
    {
        public string Name => "Handy Tag";

        public string Description => "Gives $1 per played hand this run [Will give" + App.CurrentGame.Player.TotalPlayedHandsCount + "]";

        public BitmapImage Image => new BitmapImage(new Uri("ms-appx:///Assets/TagImages/Handy_Tag.png"));

        public int MinAnte => 2;

        public void ApplyEffect(Player Player)
        {
            Player.Money += Player.TotalPlayedHandsCount;
        }

        public void ApplyEffect(Shop Shop)
        {
            return;
        }

        public void ApplyEffect(BalatroGame Game)
        {
            return;
        }
    }
}
