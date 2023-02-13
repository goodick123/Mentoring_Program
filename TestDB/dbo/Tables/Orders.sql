CREATE TABLE [dbo].[Orders] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [Status]     VARCHAR(50)      NOT NULL,
    [CreateDate] DATETIME NOT NULL,
    [UpdateDate] DATETIME NULL,
    [ProductId]  INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_ToProducts] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
);

