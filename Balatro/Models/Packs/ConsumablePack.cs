using Balatro.Enums;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Packs
{
    public class ConsumablePack<T>
    {
        public BitmapImage Image { get; }
        public PackType PackType { get; }
        public string Name { get; }
        public string Description { get; }
        public int ChooseUpTo { get; }
        public List<T> PackContent { get; }
        public int Price { get; }
        public ConsumablePack(Uri Uri, int PackSize, Func<T> GetFunction, int ChooseUpTo, string Name, 
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

        public List<T> FillPack(int PackSize, Func<T> GetFunction)
        {
            List<T> PackContent = new();

            do
            {
                PackContent.Add(GetFunction());
            } while (PackContent.Count != PackSize);

            return PackContent;
        }
    }
}
