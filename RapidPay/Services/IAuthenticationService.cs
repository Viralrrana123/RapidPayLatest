using RapidPay.Data.Model;

namespace RapidPay.Services
{
    public interface IAuthenticationService
    {
        Task<AuthTicket> LoginAsync(string username, string password);
    }
}
