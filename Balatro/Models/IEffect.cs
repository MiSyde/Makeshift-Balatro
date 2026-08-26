using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models
{
    public interface IEffect
    {
        ImageSource Image { get; }
        public abstract void AddEffect(Player Player);
    }
}
