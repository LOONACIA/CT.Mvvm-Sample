using Common.Contracts;

namespace Services;
public class LoginService : ILoginService
{
	public async Task<bool> TryLoginAsync(string id, string password)
	{
		TimeSpan delay = TimeSpan.FromSeconds(2);
		await Task.Delay(delay);

		return true;
	}
}