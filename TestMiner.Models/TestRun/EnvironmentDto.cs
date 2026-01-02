namespace TestMiner.Models.TestRun;

public class EnvironmentDto : IEnvironmentDto
{
    public string MachineName { get; init; } = "Unknown";

    public string User { get; init; } = "Unknown";

    public override string ToString()
    {
        return $"{User}@{MachineName}";
    }
}
