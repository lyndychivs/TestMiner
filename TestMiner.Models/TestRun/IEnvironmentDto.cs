namespace TestMiner.Models.TestRun
{
    public interface IEnvironmentDto
    {
        string MachineName { get; init; }

        string User { get; init; }

        string ToString();
    }
}