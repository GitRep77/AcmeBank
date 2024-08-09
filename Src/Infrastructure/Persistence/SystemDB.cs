using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class SystemDB
    {
        private static readonly SystemDB _instance = new SystemDB();
        private Dictionary<long, Account> _accounts;

        public static SystemDB Instance => _instance;

        private SystemDB()
        {
            _accounts = new Dictionary<long, Account>();
            InitializeAccounts();
        }

        private void InitializeAccounts()
        {
            _accounts = new Dictionary<long, Account>
            {
                { 1, new SavingsAccount(1, "1", 2000) },
                { 2, new SavingsAccount(2, "2", 5000) },
                { 3, new CurrentAccount(3, "3", 1000, 10000) },
                { 4, new CurrentAccount(4, "4", -5000, 20000) }
            };
        }

        public void Reset()
        {
            InitializeAccounts();
        }

        public Account? GetAccountById(long accountId)
        {
            // Attempt to retrieve the account by its ID from the in-memory database.
            _accounts.TryGetValue(accountId, out var account);
            return account;
        }

        public void AddAccount(Account account)
        {
            // Adding a new account to the in-memory database.
            // The account is identified by its unique AccountId.
            _accounts.TryAdd(account.AccountId, account);
        }
    }
}
