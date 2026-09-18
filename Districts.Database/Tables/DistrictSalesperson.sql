CREATE TABLE [dbo].[DistrictSalesperson]
(
    [DistrictId]    INT          NOT NULL,
    [SalespersonId] INT          NOT NULL,
    [Role]          NVARCHAR(50) NOT NULL,

    CONSTRAINT [PK_DistrictSalesperson]
        PRIMARY KEY ([DistrictId], [SalespersonId]),

    CONSTRAINT [FK_DistrictSalesperson_District]
        FOREIGN KEY ([DistrictId])
            REFERENCES [dbo].[District] ([Id]),

    CONSTRAINT [FK_DistrictSalesperson_Salesperson]
        FOREIGN KEY ([SalespersonId])
            REFERENCES [dbo].[Salesperson] ([Id]),

    CONSTRAINT [CK_DistrictSalesperson_Role]
        CHECK ([Role] IN ('Primary', 'Secondary'))
);