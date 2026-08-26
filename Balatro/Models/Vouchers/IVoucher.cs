using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Vouchers
{
    public interface IVoucher
    {
        public string Id { get; }
        public string Description { get; }
        public ImageSource Image { get; }

        public abstract void ApplyEffect(Shop shop);
    }
}
