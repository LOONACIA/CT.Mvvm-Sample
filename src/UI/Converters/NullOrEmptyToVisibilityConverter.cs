using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UI.Converters;
public class NullOrEmptyToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is not string str)
		{
			str = value?.ToString() ?? string.Empty;
		}

		return string.IsNullOrWhiteSpace(str) ? Visibility.Collapsed : Visibility.Visible;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
