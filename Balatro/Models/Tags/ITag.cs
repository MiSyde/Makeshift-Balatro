using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tags
{
    public interface ITag
    {
        public string Name { get; }
        public string Description { get; }
        public ImageSource Image { get; }
        public int MinAnte { get; }
        public abstract void ApplyEffect(Player Player);
        public abstract void ApplyEffect(Shop Shop);
        public abstract void ApplyEffect(BalatroGame Game);
    }
}
