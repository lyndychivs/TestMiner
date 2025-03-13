CREATE TABLE [dbo].[TestExecutions]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [TestId] INT NOT NULL,
    [TestResultId] TINYINT NOT NULL,
    [TestRunId] INT NOT NULL,
    [StartTime] DATETIME NOT NULL,
    [EndTime] DATETIME NOT NULL,
    [Duration] BIGINT NOT NULL,
    [Seed] BIGINT NOT NULL,
    [Label] NVARCHAR(500) NULL,
    [AssertCount] INT NOT NULL,
    [Reason] NVARCHAR(500) NULL,
    [FailureMessage] NVARCHAR(500) NULL,
    [StackTrace] NVARCHAR(2000) NULL,
    CONSTRAINT [FK_TestExecutions_Test] FOREIGN KEY ([TestId]) REFERENCES [dbo].[Tests] ([Id]),
    CONSTRAINT [FK_TestExecutions_TestResult] FOREIGN KEY ([TestResultId]) REFERENCES [dbo].[TestResults] ([Id]),
    CONSTRAINT [FK_TestExecutions_TestRun] FOREIGN KEY ([TestRunId]) REFERENCES [dbo].[TestRuns] ([Id])
)