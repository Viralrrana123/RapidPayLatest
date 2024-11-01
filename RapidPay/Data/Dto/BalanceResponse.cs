namespace RapidPay.Data.Dto
{
    public class BalanceResponse
    {
        public double CurrentBalance { get; set; }
        public double AvailableCredit { get; set; }
        public double TotalCreditLimit { get; set; }
    }
}
