CREATE PROCEDURE [dbo].[spTestExecutions_AddTest]
    @testRunId INT,
    @name NVARCHAR(500),
    @class NVARCHAR(500),
    @result NVARCHAR(12),
    @seed BIGINT,
    @label NVARCHAR(500) = NULL,
    @startTime DATETIME,
    @endTime DATETIME,
    @duration BIGINT,
    @assertCount INT,
    @failureMessage NVARCHAR(500) = NULL,
    @stackTrace NVARCHAR(2000) = NULL,
    @reason NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @testResultId TINYINT = (SELECT [Id] FROM [dbo].[TestResults] WHERE [Result] = @result)
    IF @testResultId IS NULL
    BEGIN
        INSERT INTO [dbo].[TestResults] ([Result]) VALUES (@result)
        SET @testResultId = SCOPE_IDENTITY()
    END

    DECLARE @testClassId INT = (SELECT [Id] FROM [dbo].[TestClasses] WHERE [Class] = @class)
    IF @testClassId IS NULL
    BEGIN
        INSERT INTO [dbo].[TestClasses] ([Class]) VALUES (@class)
        SET @testClassId = SCOPE_IDENTITY()
    END

    DECLARE @testId INT = (SELECT [Id] FROM [dbo].[Tests] WHERE [Name] = @name AND [TestClassId] = @testClassId)
    IF @testId IS NULL
    BEGIN
        INSERT INTO [dbo].[Tests] ([TestClassId], [Name]) VALUES (@testClassId, @name)
        SET @testId = SCOPE_IDENTITY()
    END

    INSERT INTO [dbo].[TestExecutions] ([TestId], [TestResultId], [TestRunId], [StartTime], [EndTime], [Duration], [Seed], [Label], [AssertCount], [Reason], [FailureMessage], [StackTrace])
    VALUES (@testId, @testResultId, @testRunId, @startTime, @endTime, @duration, @seed, @label, @assertCount, @reason, @failureMessage, @stackTrace)
END