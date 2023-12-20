using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Manoir.ShoppingTools.Windows.Helpers
{
    public class BoolToOpacityConverter : IValueConverter
    {
        public double ValueIfTrue { get; set; } = 1.0;
        public double ValueIfFalse { get; set; } = .0;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var b = (bool)value;

            return b ? ValueIfTrue : ValueIfFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
