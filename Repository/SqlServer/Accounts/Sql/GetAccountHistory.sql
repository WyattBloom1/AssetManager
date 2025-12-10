SELECT 
	HistoryId,
	AccountId,
	CurrentBalance,
	ChangeDate
FROM AccountHistory
WHERE AccountId = @AccountId