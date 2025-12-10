SELECT 
	TransactionId,
	AccountId,
	Amount,
	TransactionDate,
	UserId,
	CategoryId,
	TransactionDescription
FROM Transactions
WHERE TransactionId = @Transaction