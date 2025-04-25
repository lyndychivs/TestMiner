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

            return Parser.Default.ParseArguments<MineOptions, SaveOptions>(args)
                .MapResult(
                (IMineOptions mineOptions) => RunWithOptions(mineOptions),
                (ISaveOptions saveOptions) => SaveConnectionString(saveOptions),
                (errors) => 1);
        }

        private static int RunWithOptions(IMineOptions testMinerOptions)
        {
            string connectionString = new ConnectionManager().GetConnectionString(testMinerOptions.ConnectionString);

            return new TestMinerApplication(connectionString).ProcessFiles(testMinerOptions.ReportFilePaths);
        }

        private static int SaveConnectionString(ISaveOptions saveOptions)
        {
            return new ConnectionManager().SaveConnectionString(saveOptions.ConnectionString);
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