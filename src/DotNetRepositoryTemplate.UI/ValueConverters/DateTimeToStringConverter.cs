using System;
using System.Globalization;
using System.Windows.Data;

namespace DotNetRepositoryTemplate.UI.ValueConverters;

public sealed class DateTimeToStringConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DateTime dateTime)
        {
            return string.Empty;
        }

        return dateTime.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string str)
        {
            return Binding.DoNothing;
        }

        if (DateTime.TryParseExact(str, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
        {
            return dateTime;
        }

        return Binding.DoNothing;
    }
}
