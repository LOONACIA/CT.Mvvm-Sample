using Common.Contracts;

namespace Services;
public class PaymentService : IPaymentService
{
	public async Task<bool> TryPayAsync(int totalPrice, CancellationToken cancellationToken)
	{
		TimeSpan delay = TimeSpan.FromSeconds(3);
		await Task.Delay(delay, cancellationToken);

		return true;
	}
}
