using Balatro.Models.Achievement;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models.Vouchers
{
    [RequiresAchievement("Atleast5Modifier")]
    public class Glow_Up : IVoucher
    {
        public string Id { get; }
        public string Description { get; }
        public int Price { get; set; } = 10;
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

        public Glow_Up()
        {
            Id = "Glow up";
            Description = "Foil, Holographic, and Polychrome cards appear 4x more often ";
            Image = new BitmapImage(new Uri("ms-appx:///Assets/VoucherImages/Glow_Up.png"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void ApplyEffect(Shop shop)
        {
            
        }

        public void ApplyEffect(Player Player) { return; }
        public void ApplyEffect(BalatroGame Game) { return; }
    }
}
