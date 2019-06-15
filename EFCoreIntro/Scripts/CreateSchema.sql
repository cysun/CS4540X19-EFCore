IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [DateHired] datetime2 NOT NULL,
    [SupervisorId] int NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_Employees_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [Employees] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Projects] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [LeaderId] int NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Projects_Employees_LeaderId] FOREIGN KEY ([LeaderId]) REFERENCES [Employees] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Employees_SupervisorId] ON [Employees] ([SupervisorId]);

GO

CREATE INDEX [IX_Projects_LeaderId] ON [Projects] ([LeaderId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190615183926_InitialSchema', N'2.2.4-servicing-10062');

GO

