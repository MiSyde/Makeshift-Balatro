using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Stakes
{
    public interface IStake
    {
        public BitmapImage Image { get; }
        public string Name { get; }
        public string Description { get; }
        public void ApplyStake(BalatroGame Game, Shop Shop);
    }
}
