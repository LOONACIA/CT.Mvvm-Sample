using Common.Contracts;
using System.Windows;

namespace Entry.Services;
internal class DialogService : IDialogService
{
	public void ShowMessageBox(string message)
	{
		MessageBox.Show(message);
	}
}