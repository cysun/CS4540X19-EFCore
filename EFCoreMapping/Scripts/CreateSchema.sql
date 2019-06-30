IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Customers] (
    [CustomerId] int NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Address_Street] nvarchar(max) NULL,
    [Address_City] nvarchar(max) NULL,
    [Address_State] nvarchar(max) NULL,
    [Address_Zip] char(5) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
);

GO

CREATE TABLE [Accounts] (
    [AccountId] int NOT NULL IDENTITY,
    [Balance] decimal(18,2) NOT NULL,
    [DateCreated] datetime2 NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    [OwnerId] int NULL,
    [Deleted] bit NOT NULL DEFAULT 0,
    [Discriminator] nvarchar(max) NOT NULL,
    [Term] int NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountId]),
    CONSTRAINT [FK_Accounts_Customers_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Phones] (
    [CustomerId] int NOT NULL,
    [Number] nvarchar(32) NOT NULL,
    CONSTRAINT [PK_Phones] PRIMARY KEY ([CustomerId], [Number]),
    CONSTRAINT [FK_Phones_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE
);

GO

CREATE TABLE [AccountCoOwners] (
    [AccountId] int NOT NULL,
    [CoOwnerId] int NOT NULL,
    CONSTRAINT [PK_AccountCoOwners] PRIMARY KEY ([AccountId], [CoOwnerId]),
    CONSTRAINT [FK_AccountCoOwners_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccountCoOwners_Customers_CoOwnerId] FOREIGN KEY ([CoOwnerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AccountCoOwners_CoOwnerId] ON [AccountCoOwners] ([CoOwnerId]);

GO

CREATE INDEX [IX_Accounts_OwnerId] ON [Accounts] ([OwnerId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190630154428_InitialSchema', N'2.2.4-servicing-10062');

GO

