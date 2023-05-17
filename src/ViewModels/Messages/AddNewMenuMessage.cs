using Models;

namespace ViewModels.Messages;
public class AddNewMenuMessage
{
	public AddNewMenuMessage(Menu menu)
	{
		Menu = menu;
	}

	public Menu Menu { get; }
}