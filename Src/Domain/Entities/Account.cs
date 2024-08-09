namespace Domain.Entities
{
    public abstract class Account
    {
        public long AccountId { get; protected set; }
        public string? CustomerNumber { get; protected set; }
        public int Balance { get; protected set; }

        public abstract void Withdraw(int amount);
        public abstract void Deposit(int amount);
    }
}
