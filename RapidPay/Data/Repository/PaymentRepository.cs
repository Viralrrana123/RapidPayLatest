using RapidPay.Data.Model;

namespace RapidPay.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        IStoreContext storeContext;
        public PaymentRepository(IStoreContext storeContext) {
            this.storeContext = storeContext;
        }
        public async Task<IEnumerable<Ledger>> GetLedgerForCardNumberAsync(string cardNumber)
        {
            return storeContext.Ledgers().Where(x=>x.CardNumber == cardNumber).ToList();            
        }
        public async Task SaveLedgerAsync(Ledger ledger)
        {
            storeContext.Ledgers().Add(ledger);
        }
    }
}
