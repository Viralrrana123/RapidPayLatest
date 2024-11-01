using RapidPay.Data.Model;
using RapidPay.Data.Repository;

namespace RapidPay.Services
{
    public class CardService : ICardService
    {        
        private readonly ICardRepository cardRepository;
        private readonly ICustomerRepository customerRepository;
        public CardService(ICardRepository cardRepository, ICustomerRepository customerRepository)
        {
            this.cardRepository = cardRepository;            
            this.customerRepository = customerRepository;
        }
        public async Task<string> GenerateNewCard()
        {
            string bin = await cardRepository.GetNextUATPBINAsync();
            int cardNumber = await cardRepository.GetNextCardAsync();
            
            return bin + cardNumber.ToString("D9");
        }
        public async Task<CardDetails> CreateCardAsync(int custId)
        {
            try
            {
                Customer customer = await customerRepository.GetCustomerByIdAsync(custId);                
                if (customer != null)
                {
                    string cardnumber = await GenerateNewCard();
                    CardDetails cardDetails = new CardDetails
                    {
                        CustId = custId,
                        CardNumber = cardnumber,
                        ExpirationDate = DateTime.Today.AddYears(3).ToString("MMyyyy"),
                        CurrentBalance = 0,
                        AvailableCredit = customer.CreditLimit,
                        CreditLimit = customer.CreditLimit,
                        Active = true
                    };
                    await cardRepository.SaveCardAsync(cardDetails);
                    return cardDetails;
                }
                throw new Exception("Invalid Customer, Hint: Use custId: 1001");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
