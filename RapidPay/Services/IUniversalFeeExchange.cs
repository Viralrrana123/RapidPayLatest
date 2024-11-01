namespace RapidPay.Services
{
    public interface IUniversalFeeExchange
    {
        Task<double> GetCurrentFeeAsync();
    }
}
