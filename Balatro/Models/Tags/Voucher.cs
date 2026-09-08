using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tags
{
    public class Voucher : ITag
    {
        public string Name => "Voucher Tag";

        public string Description => "Adds one Voucher to the next shop";

        public BitmapImage Image => new BitmapImage(new Uri("ms-appx:///Assets/TagImages/Voucher_Tag.png"));

        public int MinAnte => 1;

        public void ApplyEffect(Player Player)
        {
            return;
        }

        public void ApplyEffect(Shop Shop)
        {
            ++Shop.VoucherShopSize;
            Shop.VoucherShop.Add(Shop.GetVoucher());
        }

        public void ApplyEffect(BalatroGame Game)
        {
            return;
        }
    }
}
