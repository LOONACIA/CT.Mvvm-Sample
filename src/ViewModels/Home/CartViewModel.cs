using Common.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Models;
using System.Collections.ObjectModel;
using ViewModels.Home;
using ViewModels.Messages;

namespace ViewModels;
public partial class CartViewModel : ObservableObject,
	IRecipient<AddMenuToCartMessage>
{
	private readonly IPaymentService _paymentService;

	[ObservableProperty]
	private ObservableCollection<OrderViewModel> _orders = new();

	[ObservableProperty]
	private bool _isBusy;

	public CartViewModel(IPaymentService paymentService)
	{
		_paymentService = paymentService;

		WeakReferenceMessenger.Default.Register(this);
	}

	public int TotalCount => Orders.Sum(o => o.Count);

	public int TotalPrice => Orders.Sum(o => o.TotalPrice);

	public bool CanPay => TotalCount > 0;

	public void Receive(AddMenuToCartMessage message)
	{
		var menu = message.Menu;
		AddMenu(menu);
	}

	private void AddMenu(Menu menu)
	{
		var order = GetOrCreateOrderByMenu(menu);
		IncrementCount(order);
	}

	[RelayCommand]
	private void IncrementCount(OrderViewModel order)
	{
		order.Count += 1;
	}

	[RelayCommand]
	private void DecrementCount(OrderViewModel order)
	{
		order.Count -= 1;

		if (order.Count <= 0)
		{
			order.PropertyChanged -= OnOrderPropertyChanged;
			Orders.Remove(order);
			return;
		}
	}

	private OrderViewModel GetOrCreateOrderByMenu(Menu menu)
	{
		var order = Orders.SingleOrDefault(o => o.MenuId == menu.Id);
		if (order == null)
		{
			order = new OrderViewModel(new Order
			{
				MenuId = menu.Id,
				MenuName = menu.Name,
				Price = menu.Price,
			});

			Orders.Add(order);
			order.PropertyChanged += OnOrderPropertyChanged;
		}

		return order;
	}

	[RelayCommand(CanExecute = nameof(CanPay), IncludeCancelCommand = true)]
	private async Task TryPayAsync(CancellationToken cancellationToken)
	{
		IsBusy = true;
		try
		{
			await _paymentService.TryPayAsync(TotalPrice, cancellationToken);
			WeakReferenceMessenger.Default.Send(new ShowDialogMessage("결제가 완료되었습니다."));
		}
		catch (TaskCanceledException)
		{
			WeakReferenceMessenger.Default.Send(new ShowDialogMessage("결제가 취소되었습니다."));
		}
		finally
		{
			Orders.Clear();
			IsBusy = false;
		}
	}

	private void OnOrderPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		OnPropertyChanged(nameof(TotalPrice));
		OnPropertyChanged(nameof(TotalCount));
		TryPayCommand.NotifyCanExecuteChanged();
	}
}