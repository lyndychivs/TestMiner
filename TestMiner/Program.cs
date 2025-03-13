namespace TestMiner
{
    using System;

    using CommandLine;

    using TestMiner.Options;
    using TestMiner.Utility;

    internal class Program
    {
        internal static int Main(string[] args)
        {
            TrySetTitle();

            return Parser.Default.ParseArguments<TestMinerOptions>(args).MapResult(RunWithOptions, (errors) => 1);
        }

        private static int RunWithOptions(ITestMinerOptions testMinerOptions)
        {
            string connectionString = new ConnectionManager().GetConnectionString(testMinerOptions.ConnectionString);

            return new TestMinerApplication(connectionString).ProcessFiles(testMinerOptions.ReportFilePaths);
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
    }
}