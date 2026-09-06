using Balatro.Models.Jokers;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tags
{
    public class Rare : ITag
    {
        public string Name => "Rare";

        public string Description => "The next shop will have a free Rare Joker";

        public ImageSource Image => new BitmapImage(new Uri("ms-appx:///Assets/TagImages/Rare_Tag.png"));

        public int MinAnte => 1;

        public void ApplyEffect(Player Player) => throw new NotImplementedException();

        public void ApplyEffect(Shop Shop)
        {
            IJoker Joker = Shop.GetJoker(Shop.RareJokers);
            Joker.Price = 0;
            Shop.CurrentShop.Add(Joker);
        }

        public void ApplyEffect(BalatroGame Game) => throw new NotImplementedException();
    }
}
