CREATE TABLE [dbo].[SalespersonStore]
(
    [SalespersonId] INT NOT NULL,
    [StoreId]       INT NOT NULL,

    CONSTRAINT [PK_SalespersonStore]
        PRIMARY KEY ([SalespersonId], [StoreId]),

    CONSTRAINT [FK_SalespersonStore_Salesperson]
        FOREIGN KEY ([SalespersonId])
            REFERENCES [dbo].[Salesperson] ([Id]),

    CONSTRAINT [FK_SalespersonStore_Store]
        FOREIGN KEY ([StoreId])
            REFERENCES [dbo].[Store] ([Id])
);