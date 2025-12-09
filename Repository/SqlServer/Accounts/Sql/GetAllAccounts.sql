SELECT
	AccountId,
	AccountName,
	AccountType,
	IsDebt,
	IncludeInTotal,
	AccountOwner,
	AccountBalance
FROM Accounts
-- Filter by owner if specified
WHERE (AccountOwner = @OwnerID OR @OwnerID = 0)