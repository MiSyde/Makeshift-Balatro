using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.BossBlinds
{
    public interface IBossBlind
    {
        public string Name { get; }
        public int MinAnte { get; }
        public string Description { get; }
        BitmapImage Image { get; }
        public bool MatadorCompatible { get; }
        public double BaseThresholdMultiplier { get; }
        public int EarnedMoney { get; }
        public abstract void AddEffect(BalatroGame Game);
    }
}
