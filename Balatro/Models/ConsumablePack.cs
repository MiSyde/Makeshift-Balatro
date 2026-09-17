using Balatro.Enums;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models
{
    public class ConsumablePack : INotifyPropertyChanged
    {
        public BitmapImage Image { get; }
        public PackType PackType { get; }
        public Visibility ButtonVisibility { get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonVisibility)));
                }
            }
        } = Visibility.Collapsed;
        public string Name { get; }
        public string Description { get; }
        public int ChooseUpTo { get; }
        public List<IEffect> PackContent { get; }
        public int Price { get; }
        public ConsumablePack(Uri Uri, int PackSize, Func<IEffect> GetFunction, int ChooseUpTo, string Name, 
            string Description, int Price, PackType PackType)
        {
            this.Price = Price;
            this.Name = Name;
            this.Description = Description;
            this.ChooseUpTo = ChooseUpTo;
            Image = new BitmapImage(Uri);
            this.PackType = PackType;
            FillPack(PackSize, GetFunction);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<IEffect> FillPack(int PackSize, Func<IEffect> GetFunction)
        {
            List<IEffect> PackContent = new();

            do
            {
                PackContent.Add(GetFunction());
            } while (PackContent.Count != PackSize);

            return PackContent;
        }
    }
}
