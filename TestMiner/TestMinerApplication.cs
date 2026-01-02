namespace TestMiner;

using System;
using System.Collections.Generic;
using System.Linq;

using TestMiner.DataAccessLayer;
using TestMiner.Logger;
using TestMiner.Mapping;
using TestMiner.Models.TestRun;
using TestMiner.Serializer;
using TestMiner.TestReports.NUnit3;
using TestMiner.Utility;

public class TestMinerApplication
{
    internal const string Name = "Test Miner";

    private readonly ILogWrapper _logWrapper;

    private readonly IFileWrapper _fileWrapper;

    private readonly ITestReportSerializer _testReportSerializer;

    private readonly ITestRunMapper _testRunMapper;

    private readonly ITestMinerDal _testMinerDal;

    private int _responseCode;

    public TestMinerApplication(ILogWrapper logWrapper, string connectionString)
        : this(
              logWrapper,
              new FileWrapper(logWrapper),
              new TestReportSerializer(logWrapper),
              new TestRunMapper(logWrapper),
              new TestMinerDal(logWrapper, connectionString))
    {
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

        _responseCode = 0;
    }

    public int MineFiles(IEnumerable<string> filePaths)
    {
        if (filePaths == null)
        {
            _logWrapper.Error($"{nameof(filePaths)} cannot be null.");
            return 1;
        }

        if (!filePaths.Any())
        {
            _logWrapper.Warning("No Files to mine.");
            return 0;
        }

        foreach (string filePath in filePaths)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    _logWrapper.Warning($"{nameof(filePath)} cannot be null or empty.");

                    _responseCode = 1;

                    continue;
                }

                if (!_fileWrapper.Exists(filePath))
                {
                    _logWrapper.Warning($"No File exists: {filePath}");

                    _responseCode = 1;

                    continue;
                }

                string allText = _fileWrapper.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(allText))
                {
                    _logWrapper.Warning($"No Data found in File: {filePath}");

                    _responseCode = 1;

                    continue;
                }

                ITestRunDto testRunDto;
                try
                {
                    TestRun testRun = _testReportSerializer.Deserialize(allText);

                    testRunDto = _testRunMapper.MapTestRunToDto(testRun);
                }
                catch (Exception exception)
                {
                    _logWrapper.Error(exception, $"Failed to deserialize Test Run from File: {filePath}");

                    _responseCode = 1;

                    continue;
                }

                string md5Hash = testRunDto.CalculateMd5Hash();
                if (_testMinerDal.IsTestRunPreviouslyRecorded(md5Hash))
                {
                    _logWrapper.Info($"Test Run already exists in Database: {md5Hash} : {filePath}");

                    _responseCode = 1;

                    continue;
                }

                _testMinerDal.RecordTestRun(testRunDto);

                _logWrapper.Info($"Finished mining File: {md5Hash} : {filePath}");
            }
            catch (Exception exception)
            {
                _logWrapper.Error(exception, $"Failed to mine File: {filePath}");

                _responseCode = 1;
            }
        }

        return _responseCode;
    }
}
