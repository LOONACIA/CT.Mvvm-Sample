using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Models;
using System.ComponentModel.DataAnnotations;
using ViewModels.Messages;

namespace ViewModels;
public partial class AddNewMenuViewModel : ObservableValidator
{
	[ObservableProperty]
	[Required(ErrorMessage = $"{nameof(Name)}은 필수 속성입니다.")]
	private string _name = string.Empty;

	[ObservableProperty]
	[CustomValidation(typeof(AddNewMenuViewModel), nameof(ValidatePrice))]
	private int _price = 1000;

	[RelayCommand]
	private void Add()
	{
		ValidateAllProperties();
		if (HasErrors)
		{
			return;
		}

		CheckMenuMessage checkMenuMessage = new(Name);
		bool isAlreadyAdded = WeakReferenceMessenger.Default.Send(checkMenuMessage);

		if (!isAlreadyAdded)
		{
			Menu menu = new()
			{
				Name = Name,
				Price = Price
			};

			AddNewMenuMessage addNewMenuMessage = new(menu);

			WeakReferenceMessenger.Default.Send(addNewMenuMessage);

			Name = string.Empty;
			Price = 0;
		}

		string dialogMessage = isAlreadyAdded ? "이미 추가된 메뉴입니다." : "추가되었습니다.";
		WeakReferenceMessenger.Default.Send(new ShowDialogMessage(dialogMessage));
	}

	public static ValidationResult? ValidatePrice(int price)
	{
		if (price < 1000)
		{
			return new("판매가격은 최소 1,000원 이상이어야 합니다.");
		}

		if (price % 100 != 0)
		{
			return new("판매가격은 100원 단위로 입력할 수 있습니다.");
		}

		return ValidationResult.Success;
	}

	partial void OnNameChanging(string value)
	{
		ClearErrors();
	}

	partial void OnPriceChanging(int value)
	{
		ClearErrors();
	}
}