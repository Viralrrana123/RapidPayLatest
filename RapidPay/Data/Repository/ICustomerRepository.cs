using RapidPay.Data.Model;

namespace RapidPay.Data.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(int custId);
    }
}
