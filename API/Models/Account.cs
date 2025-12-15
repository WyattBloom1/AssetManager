using System.Text.Json.Serialization;

namespace AssetManager.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int AccountType { get; set; }
        public bool IsDebt { get; set; }
        public bool IncludeInTotal { get; set; }
        public int AccountOwner {  get; set; }
        public decimal AccountBalance { get; set; }

        [JsonConstructor]
        public Account(int accountId, string accountName, int accountType, bool isDebt, bool includeInTotal, int accountOwner, decimal accountBalance)
        {
            AccountId = accountId;
            AccountName = accountName;
            AccountType = accountType;
            IsDebt = isDebt;
            IncludeInTotal = includeInTotal;
            AccountOwner = accountOwner;
            AccountBalance = accountBalance;
        }
    }
}
