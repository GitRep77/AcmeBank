using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Persistence;

namespace Application.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly SystemDB _systemDB;

        public AccountService()
        {
            _systemDB = SystemDB.Instance;
        }

        public Account GetAccountById(long accountId)
        {
            var account = _systemDB.GetAccountById(accountId);
            if (account == null)
            {
                // If no account is found with the given ID, throw a custom exception.
                // This ensures that any attempt to operate on a non-existent account is caught early.
                throw new AccountNotFoundException(accountId.ToString());
            }
            return account;
        }

        public void OpenSavingsAccount(long accountId, string customerNumber, int amountToDeposit)
        {
            var account = new SavingsAccount(accountId, customerNumber, amountToDeposit);
            _systemDB.AddAccount(account);
        }

        public void OpenCurrentAccount(long accountId, string customerNumber, int overdraftLimit)
        {
            var account = new CurrentAccount(accountId, customerNumber, 0, overdraftLimit);
            _systemDB.AddAccount(account);
        }

        public void Withdraw(long accountId, int amountToWithdraw)
        {
            var account = _systemDB.GetAccountById(accountId);

            if (account == null)
            {
                // Throwing an exception here prevents further operations on a non-existent account,
                // safeguarding the integrity of the system.
                throw new AccountNotFoundException(accountId.ToString());
            }

            // Delegating the withdrawal process to the specific account type.
            // Each account type (Savings or Current) handles its own withdrawal logic.
            account.Withdraw(amountToWithdraw);
        }

        public void Deposit(long accountId, int amountToDeposit)
        {
            var account = _systemDB.GetAccountById(accountId);

            if (account == null)
            {
                throw new AccountNotFoundException(accountId.ToString());
            }

            account.Deposit(amountToDeposit);
        }
    }
}
