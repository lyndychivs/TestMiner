CREATE VIEW [dbo].[vTestRuns] AS
SELECT
    tr.Id as 'TestRunId',
    tr.Total as 'Total',
    tr.Inconclusive as 'Inconclusive',
    tr.Passed as 'Passed',
    tr.Warning as 'Warning',
    tr.Skipped as 'Skipped',
    tr.Failed as 'Failed',
    tr.Error as 'Error',
    tr.StartTime as 'StartTime',
    tr.EndTime as 'EndTime',
    tr.Duration as 'Duration',
    eu.[User] as 'User',
    em.Machine as 'Machine',
    tr.Hex as 'TestRunHex',
    tms.Status as 'TestMinerStatus'
FROM [dbo].[TestRuns] tr
    INNER JOIN [dbo].[EnvironmentUsers] AS eu ON tr.UserId = eu.Id
    INNER JOIN [dbo].[EnvironmentMachines] AS em ON tr.MachineId = em.Id
    INNER JOIN [dbo].[TestMinerStatus] AS tms ON tr.TestMinerStatusId = tms.Id