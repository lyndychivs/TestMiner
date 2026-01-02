namespace TestMiner;

using System;

using CommandLine;

using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

using TestMiner.Logger;
using TestMiner.Options;
using TestMiner.Utility;

internal class Program
{
    internal static int Main(string[] args)
    {
        TrySetTitle();

        var logWrapper = CreateLogWrapper();

        return Parser.Default.ParseArguments<MineOptions, SaveOptions>(args)
            .MapResult(
            (IMineOptions mineOptions) => RunWithOptions(mineOptions, logWrapper),
            (ISaveOptions saveOptions) => SaveConnectionString(saveOptions, logWrapper),
            (errors) => 1);
    }

    private static int RunWithOptions(IMineOptions testMinerOptions, ILogWrapper logWrapper)
    {
        string connectionString = new ConnectionManager(logWrapper).GetConnectionString(testMinerOptions.ConnectionString);

        if (!new ConnectionStringValidator(logWrapper).IsConnectionStringValid(connectionString))
        {
            return 1;
        }

        return new TestMinerApplication(logWrapper, connectionString).MineFiles(testMinerOptions.ReportFilePaths);
    }

    private static int SaveConnectionString(ISaveOptions saveOptions, ILogWrapper logWrapper)
    {
        return new ConnectionManager(logWrapper).SaveConnectionString(saveOptions.ConnectionString);
    }

    private static void TrySetTitle()
    {
        try
        {
            Console.Title = TestMinerApplication.Name;
        }
        catch
        {
        }
    }

    private static LogWrapper CreateLogWrapper()
    {
        return new LogWrapper(new SerilogLoggerFactory(
            new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Information)
            .WriteTo.File(
                $"Logs\\{nameof(TestMiner)}.log",
                restrictedToMinimumLevel: LogEventLevel.Verbose,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 3)
            .CreateLogger()).CreateLogger<ILogWrapper>());
    }
}
