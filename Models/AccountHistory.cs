using System.Text.Json.Serialization;

namespace AssetManager.Models
{
    public class AccountHistory
    {
        public int HistoryId { get; set; }
        public int AccountId { get; set; }
        public float CurrentBalance { get; set; }
        public DateTime ChangeDate { get; set; }

        [JsonConstructor]
        public AccountHistory(
            int historyId, 
            int accountId, 
            float currentBalance, 
            DateTime changeDate
        )
        {
            HistoryId = historyId;
            AccountId = accountId;
            CurrentBalance = currentBalance;
            ChangeDate = changeDate;
        }
    }
}
