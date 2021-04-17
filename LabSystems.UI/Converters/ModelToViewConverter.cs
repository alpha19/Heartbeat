using Prism.Ioc;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LabSystems.UI.Converters
{
    public class ModelToViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var viewName = $"{value}View";
            foreach (var type in GetType().Assembly.GetTypes())
            {
                if (type.Name != viewName) continue;
                return ContainerLocator.Container.Resolve(type);
            }

            throw new NotSupportedException($"Unable to find {viewName} in {GetType().Assembly}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
