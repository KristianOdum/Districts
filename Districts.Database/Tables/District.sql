CREATE TABLE dbo.District
(
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    PrimarySalespersonId INT NOT NULL,

    CONSTRAINT PK_District
        PRIMARY KEY (Id),

    CONSTRAINT UQ_District_Name
        UNIQUE (Name),

    CONSTRAINT FK_District_PrimarySalesperson
        FOREIGN KEY (PrimarySalespersonId)
            REFERENCES dbo.Salesperson(Id)
);