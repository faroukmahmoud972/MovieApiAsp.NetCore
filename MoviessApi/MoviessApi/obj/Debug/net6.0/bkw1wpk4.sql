BEGIN TRANSACTION;
GO

CREATE TABLE [Movies] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(255) NOT NULL,
    [Year] int NOT NULL,
    [rate] float NOT NULL,
    [storyline] nvarchar(2500) NOT NULL,
    [Poster] varbinary(max) NOT NULL,
    [GenreId] tinyint NOT NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Movies_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Movies_GenreId] ON [Movies] ([GenreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230107104906_addmoviestable', N'6.0.1');
GO

COMMIT;
GO

