using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Manoir.ShoppingTools.Windows.Helpers
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public bool InvertValue { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var b = (bool)value;

            if(! InvertValue)
                return b ? Visibility.Visible : Visibility.Collapsed;
            else
                return b ? Visibility.Collapsed: Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
