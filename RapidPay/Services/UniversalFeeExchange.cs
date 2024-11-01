using System;

namespace RapidPay.Services
{
    public class UniversalFeeExchange : IUniversalFeeExchange
    {
        private double currentfee;
        private DateTime lastFeeUpdated;

        public UniversalFeeExchange()
        {
            Random random = new Random();
            currentfee = Math.Round(random.NextDouble() * 2,2);
            lastFeeUpdated = DateTime.Now;
        }
        public async Task<double> GetCurrentFeeAsync()
        {
            if ((DateTime.Now - lastFeeUpdated).TotalHours < 1)
                return currentfee;
            Random random = new Random();
            double randomDecimal = Math.Round(random.NextDouble() * 2,2);
            currentfee = Math.Round(currentfee * randomDecimal,2);
            lastFeeUpdated = DateTime.Now;
            return currentfee;
        }
    }
}
