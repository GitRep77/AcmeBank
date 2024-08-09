using Domain.Exceptions;

namespace Domain.Entities
{
    public class SavingsAccount : Account
    {
        private const int MinimumBalance = 1000;

        public SavingsAccount(long accountId, string customerNumber, int initialDeposit)
        {
            if (initialDeposit < MinimumBalance)
            {
                // Ensuring that the minimum balance is respected during account creation.
                // This is a business rule specific to savings accounts.
                throw new InvalidOperationException("Minimum deposit for savings account is R1000.");
            }

            AccountId = accountId;
            CustomerNumber = customerNumber;
            Balance = initialDeposit;
        }

        public override void Withdraw(int amount)
        {
            if (Balance - amount < MinimumBalance)
            {
                // Prevent withdrawal that would reduce the balance below the required minimum.
                // This protects the account from violating the business rule.
                throw new WithdrawalAmountTooLargeException(amount, Balance);
            }

            Balance -= amount;
        }

        public override void Deposit(int amount)
        {
            Balance += amount;
        }
    }
}
