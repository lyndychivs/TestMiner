CREATE TABLE [dbo].[TestMinerStatus]
(
    [Id] TINYINT NOT NULL PRIMARY KEY IDENTITY,
    [Status] NVARCHAR(10) NOT NULL,
)
GO
CREATE INDEX [IX_TestMinerStatus_Status] ON [dbo].[TestMinerStatus] ([Status])