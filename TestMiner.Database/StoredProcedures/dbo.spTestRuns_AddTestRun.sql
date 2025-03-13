CREATE PROCEDURE [dbo].[spTestRuns_AddTestRun]
    @startTime DATETIME,
    @endTime DATETIME,
    @duration BIGINT,
    @total INT,
    @inconclusive INT,
    @passed INT,
    @warning INT,
    @skipped INT,
    @failed INT,
    @error INT,
    @user NVARCHAR(200),
    @machine NVARCHAR(200),
    @hex NVARCHAR(32)
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @userId INT = (SELECT [Id] FROM [dbo].[EnvironmentUsers] WHERE [User] = @user)
    IF @userId IS NULL
    BEGIN
        INSERT INTO [dbo].[EnvironmentUsers] ([User]) VALUES (@user)
        SET @userId = SCOPE_IDENTITY()
    END

    DECLARE @machineId INT = (SELECT [Id] FROM [dbo].[EnvironmentMachines] WHERE [Machine] = @machine)
    IF @machineId IS NULL
    BEGIN
        INSERT INTO [dbo].[EnvironmentMachines] ([Machine]) VALUES (@machine)
        SET @machineId = SCOPE_IDENTITY()
    END

    INSERT INTO [dbo].[TestRuns] ([StartTime], [EndTime], [Duration], [Total], [Inconclusive], [Passed], [Warning], [Skipped], [Failed], [Error], [UserId], [MachineId], [Hex], [TestMinerStatusId])
    VALUES (@startTime, @endTime, @duration, @total, @inconclusive, @passed, @warning, @skipped, @failed, @error, @userId, @machineId, @hex, 1)
    DECLARE @testRunId INT = SCOPE_IDENTITY()

    SELECT @testRunId AS [TestRunId]
END