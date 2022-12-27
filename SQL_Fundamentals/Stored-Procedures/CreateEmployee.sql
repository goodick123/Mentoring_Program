CREATE PROCEDURE [dbo].[CreateEmployee]
	@EmployeeName nvarchar(100) = NULL,
	@FirstName nvarchar(50) = NULL,
	@LastName nvarchar(50) = NULL,
	@CompanyName nvarchar(20),
	@Position nvarchar(30) = NULL,
	@Street nvarchar(50),
	@City nvarchar(20) = NULL,
	@State nvarchar(50) = NULL,
	@ZipCode nvarchar(50) = NULL
AS
	BEGIN
		IF (TRIM(@EmployeeName) IS NULL)
			BEGIN
				IF (TRIM(@FirstName) IS NULL AND TRIM(@LastName) IS NULL)
					BEGIN
						RAISERROR('Invalid parameters: At least one of this params would be initialized @EmployeeName or @FirstName or @LastName.', 0, 0)
						RETURN
					END
				ELSE
					BEGIN
						SET @EmployeeName = TRIM(@FirstName + ' ' + @LastName);
					END
			END

		DECLARE @PersonId INT;
		DECLARE @AddressId INT;
		DECLARE @EmployeeId INT;

		INSERT INTO Person (FirstName, LastName)
		VALUES (@FirstName, @LastName)
		SET @PersonId = SCOPE_IDENTITY()

		INSERT INTO Address (Street, City, State, ZipCode)
		VALUES (@Street,@City,@State,@ZipCode)
		SET @AddressId = SCOPE_IDENTITY()

		IF NOT EXISTS(SELECT 1 FROM [dbo].[Employee] WHERE PersonId = @PersonId AND CompanyName = @CompanyName AND EmployeeName = @EmployeeName)
		BEGIN
			INSERT INTO Employee (AddressId, PersonId, CompanyName, EmployeeName, Position)
			VALUES (@AddressId, @PersonId, @CompanyName, @EmployeeName, @Position)
			SET @EmployeeId = SCOPE_IDENTITY()
		END
	END
RETURN 0
