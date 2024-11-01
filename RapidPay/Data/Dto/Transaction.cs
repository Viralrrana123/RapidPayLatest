namespace RapidPay.Data.Dto
{
    public class Transaction
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionId { get; set; }
        public double TransactionAmount { get; set; }
        public string Description { get; set; }
        public double Fee { get; set; }

    }
}
