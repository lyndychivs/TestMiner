namespace TestMiner.Options;

using System.Collections.Generic;

using CommandLine;

[Verb("mine", false, ["m", "M"], HelpText = "Mine Test Report files to the Database.")]
internal class MineOptions : IMineOptions
{
    [Option('r', "reports", Required = true, HelpText = "\nFile paths of the NUnit3 Test Report files to upload (\"mine\") to the Database.\nCan specify multiple file paths.")]
    required public IEnumerable<string> ReportFilePaths { get; init; }

    [Option('c', "connection", Required = false, HelpText = "\nThe Connection String to the Database.")]
    public string ConnectionString { get; init; } = string.Empty;
}
