namespace TestMiner.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TestMiner.Logger;
    using TestMiner.Models.TestRun;
    using TestMiner.TestReports.NUnit3;

    internal class TestRunMapper : ITestRunMapper
    {
        private readonly ILogWrapper _logWrapper;

        internal TestRunMapper()
            : this(new LogWrapper(typeof(TestRunMapper)))
        {
        }

        internal TestRunMapper(ILogWrapper logWrapper)
        {
            _logWrapper = logWrapper ?? throw new ArgumentNullException(nameof(logWrapper));
        }

        public ITestRunDto MapTestRunToDto(TestRun testRun)
        {
            ArgumentNullException.ThrowIfNull(testRun);

            try
            {
                var testRunDto = new TestRunDto
                {
                    Environment = GetTestEnvironmentDto(testRun.TestSuites),
                    StartTime = testRun.StartDateTimeUtc,
                    EndTime = testRun.EndDateTimeUtc,
                    Duration = testRun.DurationTimeSpan,
                };

                IList<TestCase> testCases = GetTestCases(testRun.TestSuites);

                foreach (TestCase testCase in testCases)
                {
                    testRunDto.AddTest(MapTestCaseToTestRun(testCase));
                }

                return testRunDto;
            }
            catch (Exception exception)
            {
                _logWrapper.Error(exception, "Failed to map Test Run.");
                throw;
            }
        }

        private static TestDto MapTestCaseToTestRun(TestCase testCase)
        {
            var testDto = new TestDto
            {
                Name = testCase.Name,
                ClassName = testCase.ClassName,
                Result = testCase.Result.MapToResult(),
                Seed = testCase.Seed,
                StartTime = testCase.StartDateTimeUtc,
                EndTime = testCase.EndDateTimeUtc,
                Duration = testCase.DurationTimeSpan,
                Asserts = testCase.Asserts,
            };

            if (!string.IsNullOrWhiteSpace(testCase.Label))
            {
                testDto.Label = testCase.Label;
            }

            if (!string.IsNullOrWhiteSpace(testCase?.Reason?.Messages))
            {
                testDto.Reason = testCase.Reason.Messages;
            }

            if (!string.IsNullOrWhiteSpace(testCase?.Failure?.Message))
            {
                testDto.FailureMessage = testCase.Failure.Message;
            }

            if (!string.IsNullOrWhiteSpace(testCase?.Failure?.StackTrace))
            {
                testDto.StackTrace = testCase.Failure.StackTrace;
            }

            return testDto;
        }

        private IList<TestCase> GetTestCases(IList<TestSuite> testSuites)
        {
            return GetTestCases([], testSuites);
        }

        private IList<TestCase> GetTestCases(IList<TestCase> testCases, IEnumerable<Test> tests)
        {
            if (tests == null)
            {
                return testCases;
            }

            if (!tests.Any())
            {
                return testCases;
            }

            foreach (Test test in tests)
            {
                GetTestCase(testCases, test);
            }

            return testCases;
        }

        private void GetTestCase(IList<TestCase> testCases, Test test)
        {
            if (test is TestCase testCase)
            {
                testCases.Add(testCase);
            }

            if (test is TestSuite testSuite)
            {
                _ = GetTestCases(testCases, testSuite.Tests);
            }
        }

        private EnvironmentDto GetTestEnvironmentDto(IList<TestSuite> testSuites)
        {
            try
            {
                TestSuite assemblyTestSuite = testSuites.First(testSuites => testSuites.Type == TestSuiteType.Assembly);

                if (assemblyTestSuite.Environment == null)
                {
                    _logWrapper.Warning("Failed to get Test Environment Configuration.");

                    return new EnvironmentDto();
                }

                return new EnvironmentDto
                {
                    MachineName = assemblyTestSuite.Environment.MachineName,
                    User = assemblyTestSuite.Environment.User,
                };
            }
            catch (Exception exception)
            {
                _logWrapper.Warning(exception, "Failed to get Test Environment Configuration.");

                return new EnvironmentDto();
            }
        }
    }
}