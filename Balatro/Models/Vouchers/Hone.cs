using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models.Vouchers
{
    public class Hone : IVoucher
    {
        public string Id { get; }
        public string Description { get; }
        public Visibility ButtonVisibility { get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonVisibility)));
                }
            }
        } = Visibility.Collapsed;

        public BitmapImage Image { get; }

        public Hone()
        {
            Id = "Hone";
            Description = "Foil, Holographic, and Polychrome cards appear 2x more often";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/VoucherImages/Hone.png"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void ApplyEffect(Shop shop)
        {

        }

        public void ApplyEffect(Player Player) { return; }
        public void ApplyEffect(BalatroGame Game) { return; }
    }
}
