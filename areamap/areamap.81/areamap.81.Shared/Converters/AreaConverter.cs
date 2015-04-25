using System;
using Windows.UI.Xaml.Data;

namespace areamap._81.Converters
{
    public class AreaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var area = (double)value;

            if (parameter != null && parameter.ToString() == "sqft")
                area *= 10.7639104;

            return area;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}