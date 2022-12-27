CREATE TABLE [dbo].[Company]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Name] NVARCHAR(20) NOT NULL, 
    [AddressId] INT NOT NULL, 
    CONSTRAINT [FK_Company_ToAddress] FOREIGN KEY ([AddressId]) REFERENCES [Address]([Id])
)
