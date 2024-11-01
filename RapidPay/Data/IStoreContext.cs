using RapidPay.Data.Model;
using System.Collections.Concurrent;

namespace RapidPay.Data
{
    public interface IStoreContext
    {
        int NextCard();
        string NextUATPBIN();
        ConcurrentDictionary<string, User> Users();
        ConcurrentDictionary<int, Customer> Customers();
        ConcurrentDictionary<string, CardDetails> ActiveCards();
        ConcurrentBag<Ledger> Ledgers();

    }
}
