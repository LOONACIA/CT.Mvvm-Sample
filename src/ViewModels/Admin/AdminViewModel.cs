using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ViewModels.Messages;

namespace ViewModels;
public partial class AdminViewModel : ObservableObject
{
	private AddNewMenuViewModel _addNewMenuView;

	public AdminViewModel()
	{
		_addNewMenuView = Ioc.Default.GetRequiredService<AddNewMenuViewModel>();
	}

	[ObservableProperty]
	private object? _content;

	[RelayCommand]
	private void ShowAddNewMenuView()
	{
		Content = _addNewMenuView;
	}

	[RelayCommand]
	private void ExitAdminView()
	{
		WeakReferenceMessenger.Default.Send<ShowHomeViewMessage>();
	}
}