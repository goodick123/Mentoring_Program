/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
if exists(select 1 from sys.tables where name = 'Address')
begin

SET IDENTITY_INSERT Address ON
    insert into Address(Id, Street, City, State, ZipCode) 
    values (0, 'Street 1', 'Kharkiv', 'Ukraine', '12345'), 
    (1, 'Street 1', 'Kiev', 'Ukraine', '54321')
    SET IDENTITY_INSERT Address OFF

    SET IDENTITY_INSERT Company ON
    insert into Company(Id, Name, AddressId) 
    values (0, 'MacDonalds', 0)
    SET IDENTITY_INSERT Company OFF

    SET IDENTITY_INSERT Person ON
    insert into Person(Id, FirstName, LastName) 
    values (0, 'Oleksii', 'Shtanko')
     SET IDENTITY_INSERT Person OFF

     SET IDENTITY_INSERT Employee ON
    insert into Employee(Id, AddressId, PersonId, CompanyName, Position, EmployeeName) 
    values (0, 1, 0, 'MacDonalds', 'CEO', 'Oleksii Shtanko')
    SET IDENTITY_INSERT Employee OFF

end
SET IDENTITY_INSERT Address OFF
