using RapidPay.Data.Dto;
using RapidPay.Data.Model;
using RapidPay.Data.Repository;
using Transaction = RapidPay.Data.Dto.Transaction;
namespace RapidPay.Services
{
    public class PaymentService : IPaymentService
    {
        IPaymentRepository paymentRepository;
        ICardRepository cardRepository;
        IUniversalFeeExchange universalFeeExchange;
        public PaymentService(IPaymentRepository paymentRepository, ICardRepository cardRepository, IUniversalFeeExchange universalFeeExchange)
        {
            this.paymentRepository = paymentRepository;
            this.cardRepository = cardRepository;
            this.universalFeeExchange = universalFeeExchange;
        }
        public async Task EndOfDayProcessAsync(string cardNumber)
        {
            CardDetails card = await cardRepository.GetCardByCardNumberAsync(cardNumber);
            if (card != null)
            {
                IEnumerable<Ledger> ledgers = await paymentRepository.GetLedgerForCardNumberAsync(cardNumber);
                double totalCurrentPayments = ledgers.Where(x => x.TransactionDate > card.LastStatementDate).Sum(x => x.TransactionAmount);
                card.CurrentBalance = totalCurrentPayments;
                card.AvailableCredit = card.CreditLimit - totalCurrentPayments;
                await cardRepository.SaveCardAsync(card);
            }
        }
        public async Task<IEnumerable<Transaction>> GetCurrentTransactionsAsync(string cardNumber)
        {
            CardDetails card = await cardRepository.GetCardByCardNumberAsync(cardNumber);

            if (card != null)
            {
                IEnumerable<Ledger> ledgers = await paymentRepository.GetLedgerForCardNumberAsync(cardNumber);
                IEnumerable<Transaction> transactions = ledgers.Where(x => x.TransactionDate >= card.LastStatementDate).Select(x => GetTransaction(x)).ToList();
                return transactions;
            }
            return Enumerable.Empty<Transaction>();
        }
        private Transaction GetTransaction(Ledger ledger)
        {
            return new Transaction
            {
                TransactionDate = ledger.TransactionDate,
                TransactionId = ledger.TransactionId,
                TransactionAmount = ledger.TransactionAmount,
                Description = ledger.Description,
                Fee = ledger.TransationFee
            };
        }
        public async Task<BalanceResponse> GetCardBalanceAsync(string cardNumber)
        {
            CardDetails cardDetails = await cardRepository.GetCardByCardNumberAsync(cardNumber);
            if (cardDetails != null)
            {
                return new BalanceResponse
                {
                    CurrentBalance = cardDetails.CurrentBalance,
                    AvailableCredit = cardDetails.AvailableCredit,
                    TotalCreditLimit = cardDetails.CreditLimit
                };
            }
            return new BalanceResponse();
        }
        public async Task<AuthResult> PayAsync(PaymentRequest paymentRequest)
        {
            AuthResult result = new AuthResult();
            CardDetails cardDetails = await cardRepository.GetCardByCardNumberAsync(paymentRequest.CardNumber);
            if (cardDetails != null && cardDetails.AvailableCredit >= paymentRequest.TransactionAmount)
            {
                cardDetails.CurrentBalance = cardDetails.CurrentBalance + paymentRequest.TransactionAmount;
                cardDetails.AvailableCredit = cardDetails.CreditLimit - cardDetails.CurrentBalance;
                double transactionFee = await universalFeeExchange.GetCurrentFeeAsync();
                Ledger ledger = new Ledger
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    TransactionDate = DateTime.Now,
                    TransactionAmount = paymentRequest.TransactionAmount,
                    CardNumber = paymentRequest.CardNumber,
                    TransationFee = transactionFee,
                    MerchantAccountNumber = paymentRequest.MerchantAccountNumber,
                    MerchantABA = paymentRequest.MerchantABA,
                    TransType = "Debit",
                    Description = paymentRequest.Description
                };
                await paymentRepository.SaveLedgerAsync(ledger);
                await cardRepository.SaveCardAsync(cardDetails);
                result.Status = "Approved";
                result.transactionId = ledger.TransactionId;
            }
            else
                result.Status = "Declined";

            return result;
        }
    }
}
