using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ViewModels.Home;
public class OrderViewModel : ObservableObject
{
	private readonly Order _order;

	public OrderViewModel(Order order)
	{
		_order = order;
	}

	public int MenuId => _order.MenuId;

	public string? MenuName => _order.MenuName;

	public int Price => _order.Price;

	public int Count
	{
		get => _order.Count;
		set
		{
			SetProperty(_order.Count, value, _order, (order, newCount) => order.Count = newCount);
			OnPropertyChanged(nameof(TotalPrice));
		}
	}

	public int TotalPrice => Price * Count;
}