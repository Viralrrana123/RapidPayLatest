using RapidPay.Data.Model;

namespace RapidPay.Data.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserNameAsync(string username);        

    }
}
