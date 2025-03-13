namespace TestMiner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using TestMiner.DataAccessLayer;
    using TestMiner.Logger;
    using TestMiner.Mapping;
    using TestMiner.Models.TestRun;
    using TestMiner.Serializer;
    using TestMiner.TestReports.NUnit3;
    using TestMiner.Utility;

    internal class TestMinerApplication
    {
        internal const string Name = "Test Miner";

        private readonly ILogWrapper _logWrapper;

        private readonly IFileWrapper _fileWrapper;

        private readonly ITestReportSerializer _testReportSerializer;

        private readonly ITestRunMapper _testRunMapper;

        private readonly ITestMinerDal _testMinerDal;

        internal TestMinerApplication(string connectionString)
            : this(
                  new LogWrapper(typeof(TestMinerApplication)),
                  new FileWrapper(),
                  new TestReportSerializer(),
                  new TestRunMapper(),
                  new TestMinerDal(connectionString))
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        }

        internal TestMinerApplication(
            ILogWrapper logWrapper,
            IFileWrapper fileWrapper,
            ITestReportSerializer testReportSerializer,
            ITestRunMapper testRunMapper,
            ITestMinerDal testMinerDal)
        {
            _fileWrapper = fileWrapper ?? throw new ArgumentNullException(nameof(fileWrapper));
            _testReportSerializer = testReportSerializer ?? throw new ArgumentNullException(nameof(testReportSerializer));
            _testRunMapper = testRunMapper ?? throw new ArgumentNullException(nameof(testRunMapper));
            _logWrapper = logWrapper ?? throw new ArgumentNullException(nameof(logWrapper));
            _testMinerDal = testMinerDal ?? throw new ArgumentNullException(nameof(testMinerDal));
        }

        internal int ProcessFiles(IEnumerable<string> filePaths)
        {
            if (filePaths == null)
            {
                _logWrapper.Error(new ArgumentNullException(nameof(filePaths)), $"{nameof(filePaths)} cannot be null.");
                return 1;
            }

            if (!filePaths.Any())
            {
                _logWrapper.Warning("No Files to Process.");
                return 0;
            }

            foreach (string filePath in filePaths)
            {
                try
                {
                    if (!_fileWrapper.Exists(filePath))
                    {
                        _logWrapper.Warning(new FileNotFoundException(nameof(filePath), filePath), "No File Exists.");
                        continue;
                    }

                    string allText = _fileWrapper.ReadAllText(filePath);
                    if (string.IsNullOrWhiteSpace(allText))
                    {
                        _logWrapper.Warning(new InvalidDataException(nameof(filePath)), $"No Text Found in File. {filePath}");
                        continue;
                    }

                    TestRun testRun = _testReportSerializer.Deserialize(allText);

                    ITestRunDto testRunDto = _testRunMapper.MapTestRunToDto(testRun);

                    string md5Hash = testRunDto.CalculateMd5Hash();
                    if (_testMinerDal.IsTestRunPreviouslyRecorded(md5Hash))
                    {
                        _logWrapper.Info($"Test Run already exists in Database. {md5Hash} - {filePath}");
                        continue;
                    }

                    _testMinerDal.RecordTestRun(testRunDto);

                    _logWrapper.Info($"Finished Processing File with Hash: {md5Hash} - {filePath}");
                }
                catch (Exception exception)
                {
                    _logWrapper.Error(exception, $"{nameof(ProcessFiles)} Failed.");
                }
            }

            return 0;
        }
    }
}