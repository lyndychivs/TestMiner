namespace TestMiner.Options
{
    using System.Collections.Generic;

    internal interface ITestMinerOptions
    {
        IEnumerable<string> ReportFilePaths { get; }

        string ConnectionString { get; }
    }
}