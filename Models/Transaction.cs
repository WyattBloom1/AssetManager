namespace AssetManager.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string TransactionDescription { get; set; } = string.Empty;

        public Transaction(int transactionId, int accountId, decimal amount, DateTime transactionDate, int userId, int categoryId, string transactionDescription)
        {
            this.TransactionId = transactionId;
            this.AccountId = accountId;
            this.Amount = amount;
            this.TransactionDate = transactionDate;
            this.UserId = userId;
            this.CategoryId = categoryId;
            this.TransactionDescription = transactionDescription;
        }

        public Object toInputParams()
        {
            return new
            {
                TransactionId = this.TransactionId,
                AccountId = this.AccountId,
                Amount = this.Amount,
                TransactionDate = this.TransactionDate,
                UserId = this.UserId,
                CategoryId = this.CategoryId,
                TransactionDescription = this.TransactionDescription
            };
        }
    }
}
