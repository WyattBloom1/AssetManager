INSERT INTO Accounts(
    AccountName,
    AccountType,
    IsDebt,
    IncludeInTotal,
    AccountOwner,
    AccountBalance
)
VALUES (
    @AccountName,
    @AccountType,
    @IsDebt,
    @IncludeInTotal,
    @AccountOwner,
    @AccountBalance
)
SELECT @@IDENTITY AS accountId