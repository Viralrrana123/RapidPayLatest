namespace RapidPay.Data.Model
{
    public class Customer
    {
        public int CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public double CreditLimit { get; set; }
    }
}
