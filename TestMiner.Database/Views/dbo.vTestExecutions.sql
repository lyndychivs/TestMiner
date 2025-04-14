CREATE VIEW [dbo].[vTestExecutions] AS
SELECT
    te.Id as 'TestExecutionId',
    te.TestId as 'TestId',
    te.TestRunId as 'TestRunId',
    t.Name as 'Name',
    tc.Class as 'Class',
    tr.Result as 'Result',
    te.StartTime as 'StartTime',
    te.EndTime as 'EndTime',
    te.Duration as 'Duration',
    te.Seed as 'Seed',
    te.Label as 'Label',
    te.AssertCount as 'AssertCount',
    te.Reason as 'Reason',
    te.FailureMessage as 'FailureMessage',
    te.StackTrace as 'StackTrace'
FROM [dbo].[TestExecutions] AS te
    INNER JOIN [dbo].[Tests] AS t ON te.TestId = t.Id
    INNER JOIN [dbo].[TestClasses] AS tc ON t.TestClassId = tc.Id
    INNER JOIN [dbo].[TestResults] as tr ON te.TestResultId = tr.Id