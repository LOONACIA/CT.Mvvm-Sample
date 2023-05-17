using Common.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ViewModels.Messages;

namespace ViewModels;
public partial class LoginViewModel : ObservableValidator
{
	private readonly ILoginService _loginService;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(TryLoginCommand))]
	[Required(ErrorMessage = $"{nameof(Id)}는 필수 속성입니다.")]
	[RegularExpression(@"^[A|a]dmin[0-9]+", ErrorMessage = "유효한 아이디가 아닙니다.")]
	[NotifyDataErrorInfo]
	private string? _id;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(TryLoginCommand))]
	[Required(ErrorMessage = $"{nameof(Password)}는 필수 속성입니다.")]
	[MinLength(6, ErrorMessage = "비밀번호는 6자 이상 입력하세요.")]
	[NotifyDataErrorInfo]
	private string? _password;

	[ObservableProperty]
	private string? _errorMessage;

	public LoginViewModel(ILoginService loginService)
	{
		_loginService = loginService;
	}

	[MemberNotNullWhen(true, nameof(Id))]
	[MemberNotNullWhen(true, nameof(Password))]
	public bool CanLogin => !HasErrors;

	[RelayCommand(CanExecute = nameof(CanLogin))]
	private async Task TryLoginAsync()
	{
		if (!CanLogin)
		{
			return;
		}

		bool loginResult = await _loginService.TryLoginAsync(Id, Password);

		if (loginResult)
		{
			User user = new()
			{
				Id = Id,
				Password = Password,
			};
			LoginCompletedMessage message = new(user);
			WeakReferenceMessenger.Default.Send(message);
		}
	}
}