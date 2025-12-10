INSERT INTO Transactions(
	AccountId,
	Amount,
	TransactionDate,
	UserId,
	CategoryId,
	TransactionDescription
)
VALUES (
	@AccountId,
	@Amount,
	@TransactionDate,
	@UserId,
	@CategoryId,
	@TransactionDescription
)
SELECT @@IDENTITY AS transactionId