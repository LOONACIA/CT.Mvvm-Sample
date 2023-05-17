using CommunityToolkit.Mvvm.Messaging.Messages;
using Models;

namespace ViewModels.Messages;
public class LoginCompletedMessage : ValueChangedMessage<User>
{
	public LoginCompletedMessage(User value) : base(value)
	{
	}
}