namespace RapidPay.Data.Dto
{
    public class PaymentRequest
    {
        public string CardNumber { set; get; }
        public double TransactionAmount { set; get; }        
        public string MerchantAccountNumber { set; get; }
        public string MerchantABA { set; get; }        
        public string Others { set; get; }
        public string Description { set; get; }

    }
}
