using RapidPay.Data.Model;
namespace RapidPay.Data.Repository
{
    public class UserRepository: IUserRepository
    {
        IStoreContext storeContext;
        public UserRepository(IStoreContext storeContext)
        {
            this.storeContext=storeContext;
        }        
        public async Task<User> GetUserByUserNameAsync(string username)
        {            
            if (storeContext.Users().TryGetValue(username, out User user))
                return user;
            return null;
            
        }
    }
}
