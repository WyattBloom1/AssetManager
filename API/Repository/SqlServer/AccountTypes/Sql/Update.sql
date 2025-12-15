Update AccountTypes
SET 
	TypeName		= isnull(@TypeName, TypeName),
	IsDebt			= isnull(@IsDebt, IsDebt),
	TypeDescription = isnull(@TypeDescription, TypeDescription)
WHERE TypeId = @TypeId