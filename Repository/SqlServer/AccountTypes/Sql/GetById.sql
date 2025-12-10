SELECT 
	TypeId,
	TypeName,
	isDebt,
	TypeDescription
FROM AccountTypes
WHERE TypeId = @TypeId