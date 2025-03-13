namespace TestMiner.Options
{
    using System.Collections.Generic;

    using CommandLine;

    [Verb("mine", true, ["m", "M"], HelpText = "Mine Test Report files to the Database.")]
    internal class TestMinerOptions : ITestMinerOptions
    {
        [Option('r', "reports", Required = true, HelpText = "\nFile paths of the NUnit3 Test Report files to process.")]
        required public IEnumerable<string> ReportFilePaths { get; set; }

        [Option('c', "connection", Required = false, HelpText = "\nThe Connection String to the Database. (can also be specified by the Connection.json)")]
        public string ConnectionString { get; set; } = string.Empty;
    }
}