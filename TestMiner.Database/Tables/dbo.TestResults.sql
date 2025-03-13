CREATE TABLE [dbo].[TestResults]
(
    [Id] TINYINT NOT NULL PRIMARY KEY IDENTITY,
    [Result] VARCHAR(12) NOT NULL,
)
GO
CREATE INDEX [IX_TestResults_Name] ON [dbo].[TestResults] ([Result])