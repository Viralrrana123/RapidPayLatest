namespace RapidPay.Data.Model
{
    public class AuthTicket
    {
        public bool Authenticated { get; set; }=false;
        public string Token { get; set; }
    }
}
