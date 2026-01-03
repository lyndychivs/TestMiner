namespace TestMiner.Models.TestRun;

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class TestRunDto : ITestRunDto
{
    private readonly List<ITestDto> _tests;

    public TestRunDto(DateTime startTime, DateTime endTime, TimeSpan duration, IEnvironmentDto environment)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(startTime, default);
        ArgumentOutOfRangeException.ThrowIfEqual(endTime, default);
        ArgumentNullException.ThrowIfNull(environment);

        StartTime = startTime;
        EndTime = endTime;
        Duration = duration;
        Environment = environment;

        _tests = [];
    }

    public DateTime StartTime { get; private set; }

    public DateTime EndTime { get; private set; }

    public TimeSpan Duration { get; private set; }

    public int Total => Inconclusive + Passed + Warning + Skipped + Failed + Error;

    public int Inconclusive { get; private set; }

    public int Passed { get; private set; }

    public int Warning { get; private set; }

    public int Skipped { get; private set; }

    public int Failed { get; private set; }

    public int Error { get; private set; }

    public IEnvironmentDto Environment { get; private set; }

    public TestMinerStatus TestMinerStatus { get; } = TestMinerStatus.Processing;

    public IList<ITestDto> Tests => _tests.AsReadOnly();

    public void AddTest(ITestDto testDto)
    {
        ArgumentNullException.ThrowIfNull(testDto);

        _tests.Add(testDto);

        switch (testDto.Result)
        {
            case Result.Inconclusive:
                Inconclusive++;
                break;

            case Result.Passed:
                Passed++;
                break;

            case Result.Warning:
                Warning++;
                break;

            case Result.Skipped:
                Skipped++;
                break;

            case Result.Failed:
                Failed++;
                break;

            case Result.Error:
                Error++;
                break;
        }
    }

    public override string ToString()
    {
        return $"{nameof(Total)}: {Total} {nameof(Inconclusive)}: {Inconclusive} {nameof(Passed)}: {Passed} {nameof(Warning)}: {Warning} {nameof(Skipped)}: {Skipped} {nameof(Failed)}: {Failed} {nameof(Error)}: {Error} {nameof(StartTime)}: {StartTime:O} {nameof(EndTime)}: {EndTime:O} {nameof(Duration)}: {Duration} {nameof(Environment)}: {Environment}";
    }

    public string CalculateMd5Hash()
    {
        return Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(ToString())));
    }
}
