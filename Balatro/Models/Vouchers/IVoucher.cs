using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Vouchers
{
    public interface IVoucher
    {
        public string Id { get; }
        public string Description { get; }
        public BitmapImage Image { get; }

        public abstract void ApplyEffect(Player Player);
        public abstract void ApplyEffect(Shop Shop);
        public abstract void ApplyEffect(BalatroGame Game);
    }
}
