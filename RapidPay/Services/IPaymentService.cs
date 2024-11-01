using RapidPay.Data.Dto;
using RapidPay.Data.Model;

namespace RapidPay.Services
{
    public interface IPaymentService
    {
        Task<AuthResult> PayAsync(PaymentRequest paymentRequest);

        Task<BalanceResponse> GetCardBalanceAsync(string cardNumber);

        Task EndOfDayProcessAsync(string cardNumber);

        Task<IEnumerable<Transaction>> GetCurrentTransactionsAsync(string cardNumber);

    }
}
