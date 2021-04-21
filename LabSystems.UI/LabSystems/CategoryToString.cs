using System;
using System.Globalization;
using System.Windows.Data;
using LabSystems.Domain.Extensions;

namespace LabSystems.UI.LabSystems
{
    public class CategoryToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(SystemCategories.Categories.ContainsValue((SystemCategories.Category)value))
            {
                // Find the value string
                SystemCategories.Category category;
                foreach (string key in SystemCategories.Categories.Keys)
                {

                    if(SystemCategories.Categories.TryGetValue(key, out category) &&
                        category == (SystemCategories.Category)value)
                    {
                        return key;
                    }
                }
            }

            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
