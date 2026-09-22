CREATE TABLE dbo.DistrictSecondarySalesperson
(
    DistrictId    INT NOT NULL,
    SalespersonId INT NOT NULL,

    CONSTRAINT PK_DistrictSecondarySalesperson
        PRIMARY KEY (DistrictId, SalespersonId),

    CONSTRAINT FK_DistrictSecondarySalesperson_District
        FOREIGN KEY (DistrictId)
            REFERENCES dbo.District (Id),

    CONSTRAINT FK_DistrictSecondarySalesperson_Salesperson
        FOREIGN KEY (SalespersonId)
            REFERENCES dbo.Salesperson (Id)
);