namespace TestMiner.DataAccessLayer
{
    using TestMiner.Models.TestRun;

    internal interface ITestMinerDal
    {
        bool IsTestRunPreviouslyRecorded(string md5Hash);

        void RecordTestRun(ITestRunDto testRunDto);
    }
}