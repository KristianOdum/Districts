CREATE TABLE [dbo].[Salesperson]
(
    [Id]             INT IDENTITY (1,1) NOT NULL,
    [EmployeeNumber] NVARCHAR(50)       NOT NULL,
    [Name]           NVARCHAR(200)      NOT NULL,

    CONSTRAINT [PK_Salesperson]
        PRIMARY KEY ([Id]),

    CONSTRAINT [UQ_Salesperson_EmployeeNumber]
        UNIQUE ([EmployeeNumber])
);