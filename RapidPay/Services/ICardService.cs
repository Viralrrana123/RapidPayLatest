using RapidPay.Data.Model;

namespace RapidPay.Services
{
    public interface ICardService
    {
        Task<CardDetails> CreateCardAsync(int custId);
        
    }
}
