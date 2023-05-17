namespace Common.Contracts;
public interface IPaymentService
{
	Task<bool> TryPayAsync(int totalPrice, CancellationToken cancellationToken);
}
