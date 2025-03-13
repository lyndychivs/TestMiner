namespace TestMiner.Models.TestRun
{
    public class EnvironmentDto : IEnvironmentDto
    {
        public string MachineName { get; set; } = "Unknown";

        public string User { get; set; } = "Unknown";

        public override string ToString()
        {
            return $"{User}@{MachineName}";
        }
    }
}