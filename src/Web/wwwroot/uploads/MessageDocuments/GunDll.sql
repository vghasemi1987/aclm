BEGIN TRANSACTION;
GO

ALTER TABLE [MessageHandler] ADD [Address] nvarchar(256) NULL;
GO

UPDATE [ApplicationUserItems] SET [RegDateTime] = '2022-05-29T11:48:19.2968755+04:30'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220529071820_add-address-for-messagehandler', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [MessageHandler] ADD [FileName] nvarchar(100) NULL;
GO

UPDATE [ApplicationUserItems] SET [RegDateTime] = '2022-05-29T14:50:24.4187096+04:30'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220529102025_add-fileName-for-messagehandler', N'6.0.3');
GO

COMMIT;
GO

