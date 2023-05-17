using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewModels.Messages;

namespace ViewModels.Home;
public partial class MenuViewModel : ObservableRecipient,
	IRecipient<AddNewMenuMessage>,
	IRecipient<CheckMenuMessage>
{
	[ObservableProperty]
	private ObservableCollection<Menu> _menuItems = default!;

	public MenuViewModel()
	{
		LoadData();

		IsActive = true;
	}

	public void Receive(AddNewMenuMessage message)
	{
		var newMenu = message.Menu;
		newMenu.Id = MenuItems.Count + 1;
		MenuItems.Add(newMenu);
	}

	public void Receive(CheckMenuMessage message)
	{
		message.Reply(MenuItems.Any(menu => menu.Name == message.MenuName));
	}

	[RelayCommand]
	private void AddToCart(Menu menu)
	{
		Messenger.Send<AddMenuToCartMessage>(new(menu));
	}

	private void LoadData()
	{
		MenuItems = new()
		{
			new Menu { Id = 1, Name = "아메리카노", Price = 1500 },
			new Menu { Id = 2, Name = "카페라떼", Price = 2000 },
			new Menu { Id = 3, Name = "바닐라라떼", Price = 2500 },
			new Menu { Id = 4, Name = "돌체라떼", Price = 3000 },
			new Menu { Id = 5, Name = "토피넛라떼", Price = 3500 },
			new Menu { Id = 6, Name = "망고스무디", Price = 3800 },
			new Menu { Id = 7, Name = "딸기주스", Price = 3800 },
			new Menu { Id = 8, Name = "유자차", Price = 4000 },
			new Menu { Id = 9, Name = "레몬에이드", Price = 3500 },
		};
	}
}