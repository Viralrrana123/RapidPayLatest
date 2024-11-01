using RapidPay.Data.Model;

namespace RapidPay.Data.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        IStoreContext storeContext;
        public CustomerRepository(IStoreContext storeContext) { 
            this.storeContext = storeContext;
        }

        public async Task<Customer> GetCustomerByIdAsync(int custId)
        {
            if(storeContext.Customers().TryGetValue(custId, out Customer customer)) { 
                return customer;
            }
            return null;
        }
    }
}
