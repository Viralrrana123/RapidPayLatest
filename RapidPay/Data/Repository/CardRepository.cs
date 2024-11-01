using RapidPay.Data.Model;

namespace RapidPay.Data.Repository
{
    public class CardRepository : ICardRepository
    {
        IStoreContext storeContext;
        public CardRepository(IStoreContext storeContext)
        {
            this.storeContext = storeContext;
        }
        public async Task<int> GetNextCardAsync()
        {
            return storeContext.NextCard();
        }

        public async Task<string> GetNextUATPBINAsync()
        {
            return storeContext.NextUATPBIN();
        }
        public async Task SaveCardAsync(CardDetails cardDetails)
        {
            if (!storeContext.ActiveCards().ContainsKey(cardDetails.CardNumber))
            {
                storeContext.ActiveCards().TryAdd<string, CardDetails>(cardDetails.CardNumber, cardDetails);
            }
            else
            {
                storeContext.ActiveCards()[cardDetails.CardNumber] = cardDetails;
            }
        }
        public async Task<CardDetails> GetCardByCardNumberAsync(string cardNumber)
        {
            if (storeContext.ActiveCards().TryGetValue(cardNumber, out CardDetails cardDetails))
                return cardDetails;
            return null;
        }
    }
}
