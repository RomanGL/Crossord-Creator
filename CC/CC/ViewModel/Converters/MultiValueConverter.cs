using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace Crossword_Application_Modern.ViewModel.Converters
{
    /// <summary>
    /// Конвертер, предназначенный для конвертирования двух объектов в коллекцию типа String.
    /// </summary>
    public class MultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<string> result = new List<string>();
            result.Add((string)values[0]);
            result.Add((string)values[1]);
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
