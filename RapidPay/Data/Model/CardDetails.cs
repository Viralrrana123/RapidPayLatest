using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace RapidPay.Data.Model
{
    public class CardDetails
    {
        public CardDetails()
        {
            this.lastStatementDate = GetLastStatementDate();
        }
        private int statementCloseDay = 28;
        private DateTime lastStatementDate;
        public int CustId { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public double CurrentBalance { get; set; }
        public double AvailableCredit { get; set; }
        public double CreditLimit { get; set; }
        public bool Active { get; set; }
        public int StatementCloseDay
        {
            get
            {
                return statementCloseDay;
            }
            set
            {
                if (value > 28)
                    statementCloseDay = 28;
            }
        }
        public DateTime LastStatementDate
        {
            get
            {
                return GetLastStatementDate();
            }
        }
        DateTime GetLastStatementDate()
        {
            DateTime lastStatementDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, statementCloseDay);
            lastStatementDate=lastStatementDate.AddMonths(DateTime.Now.Day < statementCloseDay ? -1 : 0);
            return lastStatementDate;
        }

    }
}
