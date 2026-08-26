using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;
using Balatro.Models.Achievement;

namespace Balatro.Models.Vouchers
{
    [RequiresAchievement("Atleast5Modifier")]
    public class Glow_Up : IVoucher
    {
        public string Id { get; }
        public string Description { get; }

        public ImageSource Image { get; }

        public Glow_Up()
        {
            Id = "Glow up";
            Description = "Foil, Holographic, and Polychrome cards appear 4x more often ";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/4")); //placeholder
        }

        public void ApplyEffect(Shop shop)
        {
            
        }
    }
}
