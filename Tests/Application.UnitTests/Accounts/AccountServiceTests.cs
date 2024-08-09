using Domain.Entities;
using Domain.Exceptions;

namespace Application.UnitTests.Accounts
{
    public class AccountServiceTests : TestBase
    {
        /// <summary>
        /// Tests withdrawing from a savings account, including a case where the withdrawal would violate the minimum balance.
        /// </summary>
        [Fact]
        public void TestWithdrawFromSavingsAccount()
        {
            // Withdraw a valid amount that maintains the minimum balance
            AccountService.Withdraw(1, 500);

            // Attempt to withdraw an amount that would reduce the balance below the minimum required (R1000)
            // Expecting an exception to be thrown here as it violates the minimum balance rule
            Assert.Throws<WithdrawalAmountTooLargeException>(() => AccountService.Withdraw(1, 1600));
        }

        /// <summary>
        /// Tests withdrawing from a current account, including handling of overdraft limits.
        /// </summary>
        [Fact]
        public void TestWithdrawFromCurrentAccount()
        {
            // Withdraw within the available balance and overdraft limit
            AccountService.Withdraw(3, 2000);

            // Attempt to withdraw more than the allowed overdraft limit
            // This should throw an exception as it exceeds the overdraft limit
            Assert.Throws<WithdrawalAmountTooLargeException>(() => AccountService.Withdraw(3, 12000));
        }

        /// <summary>
        /// Tests depositing money into a savings account.
        /// </summary>
        [Fact]
        public void TestDepositToSavingsAccount()
        {
            // Deposit valid amounts into savings accounts
            AccountService.Deposit(1, 1000);
            AccountService.Deposit(2, 2000);
        }

        /// <summary>
        /// Tests depositing money into a current account.
        /// </summary>
        [Fact]
        public void TestDepositToCurrentAccount()
        {
            // Deposit valid amounts into current accounts
            AccountService.Deposit(3, 5000);
            AccountService.Deposit(4, 7000);
        }

        /// <summary>
        /// Tests attempting to withdraw from a non-existent account.
        /// </summary>
        [Fact]
        public void TestWithdrawAccountNotFound()
        {
            // Attempt to withdraw from an account that doesn't exist
            // Expecting an AccountNotFoundException to be thrown
            Assert.Throws<AccountNotFoundException>(() => AccountService.Withdraw(999, 100));
        }

        /// <summary>
        /// Tests attempting to deposit to a non-existent account.
        /// </summary>
        [Fact]
        public void TestDepositAccountNotFound()
        {
            // Attempt to deposit into an account that doesn't exist
            // Expecting an AccountNotFoundException to be thrown
            Assert.Throws<AccountNotFoundException>(() => AccountService.Deposit(999, 100));
        }

        /// <summary>
        /// Tests opening a new savings account with a valid initial deposit.
        /// </summary>
        [Fact]
        public void TestOpenSavingsAccount()
        {
            // Open a savings account and verify it was created with the correct balance
            AccountService.OpenSavingsAccount(5, "5", 1500);
            var account = AccountService.GetAccountById(5);
            Assert.NotNull(account);
            Assert.Equal(1500, account.Balance);
        }

        /// <summary>
        /// Tests opening a new current account with a specified overdraft limit.
        /// </summary>
        [Fact]
        public void TestOpenCurrentAccount()
        {
            // Open a current account and verify it was created with the correct overdraft limit
            AccountService.OpenCurrentAccount(6, "6", 5000);
            var account = AccountService.GetAccountById(6);
            Assert.NotNull(account);
            Assert.Equal(0, account.Balance);  // Initial balance should be zero
            Assert.Equal(5000, ((CurrentAccount)account).OverdraftLimit);
        }

        /// <summary>
        /// Tests enforcing the minimum balance requirement for a savings account.
        /// </summary>
        [Fact]
        public void TestSavingsAccountMinimumBalanceEnforcement()
        {
            // Attempt to withdraw an amount that would reduce the balance below the minimum required
            // Expecting an exception due to violation of the minimum balance rule
            Assert.Throws<WithdrawalAmountTooLargeException>(() => AccountService.Withdraw(1, 1500));
        }

        /// <summary>
        /// Tests withdrawing from a current account, ensuring the overdraft limit is not exceeded.
        /// </summary>
        [Fact]
        public void TestCurrentAccountOverdraftLimit()
        {
            // Withdraw up to the overdraft limit
            AccountService.Withdraw(3, 11000);

            // Attempt to withdraw more than the overdraft limit allows
            // This should throw an exception as it exceeds the allowed limit
            Assert.Throws<WithdrawalAmountTooLargeException>(() => AccountService.Withdraw(3, 1));
        }

        /// <summary>
        /// Tests withdrawing exactly the overdraft limit amount from a current account.
        /// </summary>
        [Fact]
        public void TestWithdrawExactOverdraftLimit()
        {
            // Withdraw the exact amount allowed by the overdraft limit
            AccountService.Withdraw(3, 11000);
            var account = AccountService.GetAccountById(3);
            Assert.Equal(-10000, account.Balance); // Balance should reflect the exact overdraft amount
        }

        /// <summary>
        /// Tests withdrawing down to the exact minimum balance in a savings account.
        /// </summary>
        [Fact]
        public void TestWithdrawExactMinimumBalance()
        {
            // Withdraw to reduce the balance to the exact minimum allowed
            AccountService.Withdraw(1, 1000);
            var account = AccountService.GetAccountById(1);
            Assert.Equal(1000, account.Balance); // Balance should be exactly the minimum required
        }

        /// <summary>
        /// Tests depositing zero into an account to ensure no change in balance but handled correctly.
        /// </summary>
        [Fact]
        public void TestDepositZeroAmount()
        {
            var initialBalance = AccountService.GetAccountById(1).Balance;
            AccountService.Deposit(1, 0);
            var account = AccountService.GetAccountById(1);
            Assert.Equal(initialBalance, account.Balance); // Balance should remain unchanged
        }

        /// <summary>
        /// Tests withdrawing zero from an account to ensure no change in balance but handled correctly.
        /// </summary>
        [Fact]
        public void TestWithdrawZeroAmount()
        {
            var initialBalance = AccountService.GetAccountById(1).Balance;
            AccountService.Withdraw(1, 0);
            var account = AccountService.GetAccountById(1);
            Assert.Equal(initialBalance, account.Balance); // Balance should remain unchanged
        }
    }

}
