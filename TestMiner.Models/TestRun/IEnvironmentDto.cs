namespace TestMiner.Models.TestRun
{
    public interface IEnvironmentDto
    {
        string MachineName { get; set; }

        string User { get; set; }

        string ToString();
    }
}