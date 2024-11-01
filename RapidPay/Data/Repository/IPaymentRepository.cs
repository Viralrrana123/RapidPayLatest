using RapidPay.Data.Model;

namespace RapidPay.Data.Repository
{
    public interface IPaymentRepository
    {
        Task SaveLedgerAsync(Ledger ledger);
        Task<IEnumerable<Ledger>> GetLedgerForCardNumberAsync(string cardNumber);
        
    }
}
