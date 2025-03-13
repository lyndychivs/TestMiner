CREATE PROCEDURE [dbo].[spTestRuns_UpdateTestMinerStatus]
    @testRunId INT,
    @testMinerStatusId TINYINT
AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[TestRuns]
    SET [TestMinerStatusId] = @testMinerStatusId
    WHERE [Id] = @testRunId
END