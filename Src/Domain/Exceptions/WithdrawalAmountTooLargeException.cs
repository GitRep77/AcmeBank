namespace Domain.Exceptions
{
    public class WithdrawalAmountTooLargeException : Exception
    {
        public WithdrawalAmountTooLargeException(decimal attemptedAmount, decimal balance)
            : base($"Attempted withdrawal amount of {attemptedAmount:C} exceeds available balance of {balance:C}.")
        {
        }
    }
}
