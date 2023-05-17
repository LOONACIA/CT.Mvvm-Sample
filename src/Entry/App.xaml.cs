using Common.Contracts;
using CommunityToolkit.Mvvm.DependencyInjection;
using Entry.Services;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System.Windows;
using UI.Views;
using UI.Windows;
using ViewModels;
using ViewModels.Home;

namespace Entry;
public partial class App : Application
{
	public App()
	{
		ServiceProvider = ConfigureServices();

		Ioc.Default.ConfigureServices(ServiceProvider);
	}

	public IServiceProvider ServiceProvider { get; }

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		ShellView shell = new()
		{
			DataContext = Ioc.Default.GetRequiredService<ShellViewModel>()
		};

		MainWindow win = new()
		{
			Title = "Kiosk",
			Content = shell
		};
		win.ShowDialog();
	}

	private IServiceProvider ConfigureServices()
	{
		ServiceCollection services = new();

		services.AddSingleton<ILoginService, LoginService>();
		services.AddSingleton<IDialogService, DialogService>();
		services.AddSingleton<IPaymentService, PaymentService>();

		services.AddTransient<ShellViewModel>();

		services.AddTransient<LoginViewModel>();

		services.AddTransient<HomeViewModel>();
		services.AddTransient<MenuViewModel>();
		services.AddTransient<CartViewModel>();

		services.AddTransient<AdminViewModel>();
		services.AddTransient<AddNewMenuViewModel>();

		return services.BuildServiceProvider();
	}
}