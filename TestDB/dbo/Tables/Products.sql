CREATE TABLE [dbo].[Products] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    [Weight]      FLOAT (53)    NOT NULL,
    [Length]       FLOAT (53)    NOT NULL,
    [Width]       FLOAT (53)    NOT NULL,
    [Height]      FLOAT (53)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

