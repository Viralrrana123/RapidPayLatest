namespace RapidPay.Data.Model
{
    public class AuthResult
    {
        public string transactionId { get; set; }
        public DateTime transactionDate { get; set; }= DateTime.Now;
        public string Status { get; set; } = "";

    }
}
