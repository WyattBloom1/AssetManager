INSERT INTO AccountTypes(
    TypeName,
    IsDebt,
    TypeDescription
)
VALUES (
    @TypeName,
    @IsDebt,
    @TypeDescription
)
SELECT @@IDENTITY AS typeId