Update Transactions
SET 
	TransactionId = isnull(@TransactionId, TransactionId),
	AccountId = isnull(@AccountId, AccountId),
	Amount = isnull(@Amount, Amount),
	TransactionDate = isnull(@TransactionDate, TransactionDate),
	UserId = isnull(@UserId, UserId),
	CategoryId = isnull(@CategoryId, CategoryId),
	TransactionDescription = isnull(@TransactionDescription, TransactionDescription)
WHERE TransactionId = @TransactionId