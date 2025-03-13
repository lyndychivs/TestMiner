CREATE PROCEDURE [dbo].[spTestRuns_GetIdFromHex]
    @hex NVARCHAR(32)
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @testRunId INT = (SELECT [Id] FROM [dbo].[TestRuns] WHERE [Hex] = @hex)

    IF @testRunId IS NULL
    BEGIN
        SET @testRunId = 0
    END

    SELECT @testRunId AS [TestRunId]
END