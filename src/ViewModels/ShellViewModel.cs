using Common.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Models;
using ViewModels.Messages;

namespace ViewModels;
public partial class ShellViewModel : ObservableObject,
	IRecipient<LoginCompletedMessage>,
	IRecipient<ShowHomeViewMessage>,
	IRecipient<ShowDialogMessage>
{
	private IDialogService _dialogService;

	private HomeViewModel _homeView;

	private LoginViewModel _loginView;

	private AdminViewModel _adminView;

	private bool _isLoggedIn;

	[ObservableProperty]
	private object? _content;

	[ObservableProperty]
	private User? _currentUser;

	public ShellViewModel(IDialogService dialogService)
	{
		_dialogService = dialogService;

		_homeView = Ioc.Default.GetRequiredService<HomeViewModel>();
		_loginView = Ioc.Default.GetRequiredService<LoginViewModel>();
		_adminView = Ioc.Default.GetRequiredService<AdminViewModel>();

		Content = _homeView;

		/* Register messenger scenario 1: RegisterAll */
		// WeakReferenceMessenger.Default.RegisterAll(this);

		/* Register messenger scenario 2: Register */
		WeakReferenceMessenger.Default.Register<LoginCompletedMessage>(this);
		WeakReferenceMessenger.Default.Register<ShowHomeViewMessage>(this);
		WeakReferenceMessenger.Default.Register<ShowDialogMessage>(this);
	}

	public void Receive(LoginCompletedMessage message)
	{
		User user = message.Value;
		CurrentUser = user;
		_isLoggedIn = true;
		Content = _adminView;
	}

	public void Receive(ShowHomeViewMessage message)
	{
		Content = _homeView;
	}

	public void Receive(ShowDialogMessage message)
	{
		string content = message.Message;
		if (!string.IsNullOrWhiteSpace(content))
		{
			_dialogService.ShowMessageBox(content);
		}
	}

	[RelayCommand]
	private void EnterAdminView()
	{
		Content = _isLoggedIn ? _adminView : _loginView;
	}
}