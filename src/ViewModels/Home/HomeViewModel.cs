using CommunityToolkit.Mvvm.DependencyInjection;
using ViewModels.Home;

namespace ViewModels;
public class HomeViewModel
{
	public HomeViewModel()
	{
		MenuView = Ioc.Default.GetRequiredService<MenuViewModel>();
		CartView = Ioc.Default.GetRequiredService<CartViewModel>();
	}

	public MenuViewModel MenuView { get; }

	public CartViewModel CartView { get; }
}