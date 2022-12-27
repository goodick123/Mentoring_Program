CREATE TRIGGER [AfterEmployee]
	ON [dbo].[Employee]
	AFTER INSERT
	AS
	BEGIN
			INSERT INTO Company (Name, AddressId)
			SELECT CompanyName, AddressId FROM inserted
	END
