CREATE VIEW [dbo].[EmployeeInfo]
AS 
	SELECT TOP (100) PERCENT 
	e.Id, 
	CASE WHEN e.EmployeeName IS NULL THEN p.FirstName + ' ' + p.LastName ELSE e.EmployeeName END AS 'EmployeeFullName', 
	(a.ZipCode + '-' + a.State + ', ' + a.City + '-' + a.Street) AS 'EmployeeFullAddress',
	(e.CompanyName + '(' + e.Position + ')') AS 'EmployeeCompanyInfo' FROM [Employee] AS e
	JOIN Person as p ON e.PersonId = p.Id
	JOIN [Address] as a ON e.AddressId = a.Id
	ORDER BY e.CompanyName, a.City ASC
GO