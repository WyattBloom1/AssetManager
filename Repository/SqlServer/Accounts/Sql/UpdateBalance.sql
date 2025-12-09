INSERT INTO AccountHistory (
	AccountId,
	CurrentBalance,
	ChangeDate
) VALUES (
	@AccountId,
	@AccountBalance, -- (SELECT AccountBalance FROM Accounts WHERE AccountId = @AccountId),
	GETDATE()
)

Update Accounts
SET AccountBalance = @AccountBalance
WHERE AccountId = @AccountId