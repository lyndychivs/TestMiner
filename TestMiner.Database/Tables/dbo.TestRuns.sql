CREATE TABLE [dbo].[TestRuns]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [StartTime] DATETIME NOT NULL,
    [EndTime] DATETIME NOT NULL,
    [Duration] BIGINT NOT NULL,
    [Total] INT NOT NULL,
    [Inconclusive] INT NOT NULL,
    [Passed] INT NOT NULL,
    [Warning] INT NOT NULL,
    [Skipped] INT NOT NULL,
    [Failed] INT NOT NULL,
    [Error] INT NOT NULL,
    [UserId] INT NOT NULL,
    [MachineId] INT NOT NULL,
    [Hex] NVARCHAR(32) NOT NULL,
    [TestMinerStatusId] TINYINT NOT NULL,
    CONSTRAINT [FK_TestRuns_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[EnvironmentUsers] ([Id]),
    CONSTRAINT [FK_TestRuns_Machine] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[EnvironmentMachines] ([Id]),
    CONSTRAINT [FK_TestRuns_TestMinerStatus] FOREIGN KEY ([TestMinerStatusId]) REFERENCES [dbo].[TestMinerStatus] ([Id]),
)
GO
CREATE INDEX [IX_TestRuns_Hex] ON [dbo].[TestRuns] ([Hex])