using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IAccountService
    {
        void OpenSavingsAccount(long accountId, string customerNumber, int amountToDeposit);
        void OpenCurrentAccount(long accountId, string customerNumber, int overdraftLimit);
        void Withdraw(long accountId, int amountToWithdraw);
        void Deposit(long accountId, int amountToDeposit);
        Account GetAccountById(long accountId);
    }
}
