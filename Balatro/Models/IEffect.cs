using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models
{
    public interface IEffect
    {
        public string Name { get; }
        public string Description { get; }
        BitmapImage Image { get; }
        public abstract void AddEffect(Player Player);
    }
}
