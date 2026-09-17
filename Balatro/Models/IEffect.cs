using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models
{
    public interface IEffect : INotifyPropertyChanged
    {
        public string Name { get; }
        public string Description { get; }
        BitmapImage Image { get; }
        Visibility ButtonVisibility { get; set; }
        public abstract void AddEffect(Player Player);
    }
}
