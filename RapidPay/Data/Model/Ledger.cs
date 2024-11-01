namespace RapidPay.Data.Model
{
    public class Ledger
    {
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public string  CardNumber { get; set; }
        public string MerchantAccountNumber { set; get; }
        public string MerchantABA { get; set; }        
        public double TransationFee { get; set; }
        public string TransType { get; set; }
        public string Description { get; set; }
        public string Others { get; set; }
    }
}
