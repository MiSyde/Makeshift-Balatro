using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tags
{
    public class Garbage : ITag
    {
        public string Name => "Garbage Tag";

        public string Description => "Gives $1 per unused discard this run [Will give" + App.CurrentGame.Player.TotalSavedDiscardsCount  + "]";

        public BitmapImage Image => new BitmapImage(new Uri("ms-appx:///Assets/TagImages/Garbage_Tag.png"));

        public int MinAnte => 2;

        public void ApplyEffect(Player Player)
        {
            Player.Money += Player.TotalSavedDiscardsCount;
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
