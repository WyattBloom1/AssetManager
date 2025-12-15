using System.Text.Json.Serialization;

namespace AssetManager.Models
{
    public class AccountHistory
    {
        public int HistoryId { get; set; }
        public int AccountId { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime ChangeDate { get; set; }

        //System.Int32 HistoryId, System.Int32 AccountId, System.Decimal CurrentBalance, System.DateTime ChangeDate

        [JsonConstructor]
        public AccountHistory(
            int historyId,
            int accountId,
            decimal currentBalance,
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
