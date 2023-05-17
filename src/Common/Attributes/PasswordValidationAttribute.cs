using System.ComponentModel.DataAnnotations;

namespace Common.Attributes;
public class PasswordValidationAttribute : ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value is not string password)
		{
			password = value?.ToString() ?? string.Empty;
		}

		if (string.IsNullOrWhiteSpace(password))
		{
			return new("비밀번호는 반드시 입력해야 합니다.");
		}

		if (password is not { Length: >= 6 })
		{
			return new("비밀번호는 6자 이상 입력하세요.");
		}

		return base.IsValid(value, validationContext);
	}
}