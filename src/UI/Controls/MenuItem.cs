using System.Windows;
using System.Windows.Controls;

namespace UI.Controls;
public class MenuItem : Button
{
	public static readonly DependencyProperty MenuNameProperty =
		DependencyProperty.Register(nameof(MenuName), typeof(string), typeof(MenuItem), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty PriceProperty =
		DependencyProperty.Register(nameof(Price), typeof(int), typeof(MenuItem), new PropertyMetadata(0));
	
	static MenuItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItem), new FrameworkPropertyMetadata(typeof(MenuItem)));
    }

	public string MenuName
	{
		get { return (string)GetValue(MenuNameProperty); }
		set { SetValue(MenuNameProperty, value); }
	}

	public int Price
	{
		get { return (int)GetValue(PriceProperty); }
		set { SetValue(PriceProperty, value); }
	}
}