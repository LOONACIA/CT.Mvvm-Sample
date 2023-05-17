namespace ViewModels.Messages;
public class ShowDialogMessage
{
	public ShowDialogMessage(string message)
	{
		Message = message;
	}

	public string Message { get; }
}