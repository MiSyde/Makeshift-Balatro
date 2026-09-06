using Balatro.Enums;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Jokers.Rare
{
    public class Baron : IJoker
    {
        public int Price { get; set; }

        public Rarity Rarity { get; }

        public Modifier Modifier { get; set; }

        public string Name { get; }

        public string Description { get; }

        public ImageSource Image { get; }

        public Baron()
        {
            Name = "Baron";
            Description = "Each King held in hand gives X1.5 Mult";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/JokerImages/Baron.png"));
            Price = 8;
            Modifier = Modifier.BASE;
            Rarity = Rarity.RARE;
        }

        public void AddEffect(Player Player)
        {
            foreach(Card c in Player.Cards)
            {
                if(c.FaceCardType == FaceCard.King)
                {
                    Player.Multiplier *= 1.5;
                }
            }
        }
    }
}
