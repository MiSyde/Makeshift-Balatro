using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models.Vouchers
{
    public interface IVoucher : INotifyPropertyChanged
    {
        public string Id { get; }
        public string Description { get; }
        public BitmapImage Image { get; }
        public Visibility ButtonVisibility { get; set; }
        public abstract void ApplyEffect(Player Player);
        public abstract void ApplyEffect(Shop Shop);
        public abstract void ApplyEffect(BalatroGame Game);
    }
}
