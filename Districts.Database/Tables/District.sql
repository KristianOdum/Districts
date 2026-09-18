CREATE TABLE [dbo].[District]
(
    [Id]   INT IDENTITY (1,1) NOT NULL,
    [Name] NVARCHAR(100)      NOT NULL,

    CONSTRAINT [PK_District]
        PRIMARY KEY ([Id]),

    CONSTRAINT [UQ_District_Name]
        UNIQUE ([Name])

    -- Could consider a Primary Salesperson here... 
    -- But then what about the junction table? What about the future - 2 primaries etc.?
)