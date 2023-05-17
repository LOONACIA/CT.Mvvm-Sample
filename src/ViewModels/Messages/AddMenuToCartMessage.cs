using Models;

namespace ViewModels.Messages;
public class AddMenuToCartMessage
{
	public AddMenuToCartMessage(Menu menu)
	{
		Menu = menu;
	}

	public Menu Menu { get; }
}