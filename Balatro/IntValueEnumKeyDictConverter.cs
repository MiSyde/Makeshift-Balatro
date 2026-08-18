using Cards.Balatro;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro
{
    internal class IntValueEnumKeyDictConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is Dictionary<Hand, int> dictionary && parameter is Hand hand)
            {
                return dictionary.GetValueOrDefault(hand);
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
