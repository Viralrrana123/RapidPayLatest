using RapidPay.Data.Model;
using System.Collections.Concurrent;

namespace RapidPay.Data
{
    public class StoreContext : IStoreContext
    {
        private int cardCount = 1234;
        private readonly Random rd = new Random();
        private readonly object lockObject = new object();
        private readonly ConcurrentDictionary<int, Customer> customers = new ConcurrentDictionary<int, Customer>(new[] {
            new KeyValuePair<int, Customer>(1001, new Customer{
                    FirstName = "Viral",LastName = "Rana",CustId = 1001,EmailId="viralrrana@gmail.com",CreditLimit=1000
                }),
        });
        private readonly ConcurrentDictionary<string, CardDetails> activeCards = new ConcurrentDictionary<string, CardDetails>();
        private readonly ConcurrentDictionary<string, User> users = new ConcurrentDictionary<string, User>(new[] {
            new KeyValuePair<string, User>("admin", new User{
                    UserName = "admin",Password = "admin",Id = 1,Email="test@x.com"
                }),
        });

        private readonly ConcurrentBag<Ledger> ledgers = new ConcurrentBag<Ledger>();
        
        public StoreContext()
        {
            
        }
        public int NextCard()
        {
            
            lock (lockObject)
            {                
                return (++cardCount) % 999999999;
            }
        }
        public string NextUATPBIN()
        {
            lock (lockObject)
            {
                return rd.Next(100100, 195799).ToString();
            }
        }
        public ConcurrentDictionary<int, Customer> Customers()
        {
            return customers;
        }
        public ConcurrentBag<Ledger> Ledgers()
        {
            return ledgers;
        }
        public ConcurrentDictionary<string, CardDetails> ActiveCards()
        {
            return activeCards;
        }
        public ConcurrentDictionary<string, User> Users()
        {
            return users;
        }
    }
}
