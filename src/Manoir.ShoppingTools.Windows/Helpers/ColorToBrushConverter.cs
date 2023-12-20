//using Microsoft.UI.Xaml.Data;
//using System;
//using System.Collections.Generic;
//using Windows.UI;

//namespace  Manoir.ShoppingTools.Windows.Helpers
//{
//    public class ColorToBrushConverter : IValueConverter
//    {
//        private static Dictionary<global::Windows.UI.Color, Microsoft.UI.Xaml.Media.SolidColorBrush> _cache = new Dictionary<Color, Microsoft.UI.Xaml.Media.SolidColorBrush>();

//        public object Convert(object value, Type targetType, object parameter, string language)
//        {
//            if (value is StatusPill.StatusKind)
//            {
//                switch ((StatusPill.StatusKind)value)
//                {
//                    case StatusPill.StatusKind.Success:
//                        return GetFromCache(Microsoft.UI.Colors.DarkGreen);
//                    case StatusPill.StatusKind.Failed:
//                        return GetFromCache(Microsoft.UI.Colors.Firebrick);
//                    default:
//                        return GetFromCache(Microsoft.UI.Colors.Gray);
//                }
//            }

//            if (value is System.Windows.Media.Color)
//            {
//                var cl = (System.Windows.Media.Color)value;
//                var cl2 = global::Windows.UI.Color.FromArgb(cl.A, cl.R, cl.G, cl.B);

//                return GetFromCache(cl2);
//            }
//            if (value is global::Windows.UI.Color)
//            {
//                var cl = (global::Windows.UI.Color)value;
//                return GetFromCache(cl);
//            }

//            return null;
//        }

//        private static object GetFromCache(Color cl2)
//        {
//            if (_cache.TryGetValue(cl2, out var ret))
//                return ret;

//            var br = new Microsoft.UI.Xaml.Media.SolidColorBrush(cl2);
//            _cache[cl2] = br;

//            return br;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, string language)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
