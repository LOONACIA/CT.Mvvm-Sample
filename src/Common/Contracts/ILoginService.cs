namespace Common.Contracts;
public interface ILoginService
{
	Task<bool> TryLoginAsync(string id, string password);
}
