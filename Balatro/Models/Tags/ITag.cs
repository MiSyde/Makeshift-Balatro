using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tags
{
    public interface ITag
    {
        public string Name { get; }
        public string Description { get; }
        public BitmapImage Image { get; }
        public int MinAnte { get; }
        public abstract void ApplyEffect(Player Player);
        public abstract void ApplyEffect(Shop Shop);
        public abstract void ApplyEffect(BalatroGame Game);
    }
}
