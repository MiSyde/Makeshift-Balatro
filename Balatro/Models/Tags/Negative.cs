using Balatro.Models.Jokers;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tags
{
    public class Negative : ITag
    {
        public string Name => "Negative";

        public string Description => "The next base edition Joker you find in a Shop becomes Negative (+1 joker slot) and free.";

        public ImageSource Image => new BitmapImage(new Uri("ms-appx:///Assets/TagImages/Negative_Tag.png"));

        public int MinAnte => 2;

        public void ApplyEffect(Player Player) => throw new NotImplementedException();

        public void ApplyEffect(Shop Shop)
        {
            IJoker Joker;
            int jVal = Random.Shared.Next(1, 100);
            switch (jVal)
            {
                case <= 70:
                    Joker = Shop.GetJoker(Shop.CommonJokers);
                    break;
                case > 70 and <= 95:
                    Joker = Shop.GetJoker(Shop.UncommonJokers);
                    break;
                default:
                    Joker = Shop.GetJoker(Shop.RareJokers);
                    break;
            }
            Joker.Modifier = Enums.Modifier.NEGATIVE;
            Joker.Price = 0;
            Shop.CurrentShop.Add(Joker);
        }

        public void ApplyEffect(BalatroGame Game) => throw new NotImplementedException();
    }
}
