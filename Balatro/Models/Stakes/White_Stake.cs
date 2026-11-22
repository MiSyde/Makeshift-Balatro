using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Stakes
{
    public class White_Stake : IStake
    {
        public BitmapImage Image => new(new Uri("ms-appx:///Assets/StakeImages/White_Stake.png"));

        public string Name => "White Stake";

        public string Description => "Base difficulty";

        public void ApplyStake(BalatroGame Game, Shop Shop) { return; }
    }
}
