using Domain.Exceptions;

namespace Domain.Entities
{
    public class CurrentAccount : Account
    {
        public int OverdraftLimit { get; private set; }

        public CurrentAccount(long accountId, string customerNumber, int initialBalance, int overdraftLimit)
        {
            AccountId = accountId;
            CustomerNumber = customerNumber;
            Balance = initialBalance;
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(int amount)
        {
            if (Balance + OverdraftLimit < amount)
            {
                // Ensure that the withdrawal amount does not exceed the overdraft limit.
                // This check prevents the account from going beyond the allowed overdraft.
                throw new WithdrawalAmountTooLargeException(amount, Balance + OverdraftLimit);
            }

            Balance -= amount;
        }

        public override void Deposit(int amount)
        {
            Balance += amount;
        }
    }
}