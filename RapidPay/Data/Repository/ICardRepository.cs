using RapidPay.Data.Model;

namespace RapidPay.Data.Repository
{
    public interface ICardRepository
    {
        Task<int> GetNextCardAsync();
        Task<string> GetNextUATPBINAsync();

        Task SaveCardAsync(CardDetails cardDetails);

        Task<CardDetails> GetCardByCardNumberAsync(string cardNumber);

    }
}
