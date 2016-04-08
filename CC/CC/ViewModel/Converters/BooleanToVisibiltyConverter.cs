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
    /// Конвертер Boolean в System.Windows.Visibility.
    /// True - Visible, False - Hidden.
    /// </summary>
    public class BooleanToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool flag = (bool)value;

            if (flag == true)
            { return Visibility.Visible; }
            else
            { return Visibility.Hidden; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Инвертированный конвертер Boolean в System.Windows.Visibility.
    /// True - Hidden, False - Visible.
    /// </summary>
    public class BooleanToVisibiltyInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool flag = (bool)value;

            if (flag == true)
            { return Visibility.Hidden; }
            else
            { return Visibility.Visible; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
