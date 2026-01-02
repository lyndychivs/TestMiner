namespace TestMiner.Options;

using System.Collections.Generic;

internal interface IMineOptions
{
    IEnumerable<string> ReportFilePaths { get; }

    string ConnectionString { get; }
}
