info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
      User profile is available. Using 'C:\Users\vuduc\AppData\Local\ASP.NET\DataProtection-Keys' as key repository and Windows DPAPI to encrypt keys at rest.
info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 2.0.1-rtm-125 initialized 'TripVietContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer' with options: None
IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Blogs] (
    [Id] uniqueidentifier NOT NULL,
    [BlogType] int NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [CreatedById] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [PlaceId] nvarchar(max) NOT NULL,
    [PlaceName] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [UpdatedById] uniqueidentifier NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Blogs] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181007110338_TripViet_Initial', N'2.0.1-rtm-125');

GO


