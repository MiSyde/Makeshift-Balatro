using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Vouchers
{
    public class Hone : IVoucher
    {
        public string Id { get; }
        public string Description { get; }

        public ImageSource Image { get; }

        public Hone()
        {
            Id = "Hone";
            Description = "Foil, Holographic, and Polychrome cards appear 2x more often";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/VoucherImages/Hone.png"));
        }

        public void ApplyEffect(Shop shop)
        {

        }
    }
}
