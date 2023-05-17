using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ViewModels.Messages;
public class CheckMenuMessage : RequestMessage<bool>
{
	public CheckMenuMessage(string menuName)
	{
		MenuName = menuName;
	}

	public string MenuName { get; }
}