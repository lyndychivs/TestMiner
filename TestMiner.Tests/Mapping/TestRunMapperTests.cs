namespace TestMiner.Tests.Mapping;

using System;
using System.Globalization;

using Moq;

using NUnit.Framework;

using TestMiner.Logger;
using TestMiner.Mapping;
using TestMiner.Models.TestRun;
using TestMiner.TestReports.NUnit3;

[TestFixture]
public class TestRunMapperTests
{
    private const string DateTimeString = "2025-02-01T03:04:05Z";

    private readonly Mock<ILogWrapper> _mockLogWrapper;

    private readonly TestRunMapper _testRunMapper;

    public TestRunMapperTests()
    {
        _mockLogWrapper = new Mock<ILogWrapper>();

        _testRunMapper = new TestRunMapper(_mockLogWrapper.Object);
    }

    [Test]
    public void MapTestRunToDto_NullTestRun_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _testRunMapper.MapTestRunToDto(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("testRun"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'testRun')"));
        }
    }

    [Test]
    public void MapTestRunToDto_ValidTestRun_ReturnsTestRunDto()
    {
        // Arrange
        var expectedDateTime = DateTime.ParseExact(
            DateTimeString,
            "yyyy-MM-ddTHH:mm:ssK",
            CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal);
        var testRun = new TestRun
        {
            StartTime = DateTimeString,
            EndTime = DateTimeString,
            Duration = 1,
            TestSuites = [
                new ()
                {
                    Name = "a",
                    Type = TestSuiteType.Assembly,
                    Environment = new TestEnvironment
                    {
                        User = "b",
                        MachineName = "c",
                    },
                    Tests = [
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Result = TestResult.Passed,
                            Seed = 2,
                            StartTime = DateTimeString,
                            EndTime = DateTimeString,
                            Duration = 3,
                            Asserts = 4,
                            Label = "f",
                            Reason = new TestReason
                            {
                                Message = "g",
                            },
                            Failure = new TestFailure
                            {
                                Message = "h",
                                StackTrace = "i",
                            },
                        },
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Result = TestResult.Inconclusive,
                            Seed = 2,
                            StartTime = DateTimeString,
                            EndTime = DateTimeString,
                            Duration = 3,
                            Asserts = 4,
                            Label = "f",
                            Reason = new TestReason
                            {
                                Message = "g",
                            },
                            Failure = new TestFailure
                            {
                                Message = "h",
                                StackTrace = "i",
                            },
                        },
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Result = TestResult.Warning,
                            Seed = 2,
                            StartTime = DateTimeString,
                            EndTime = DateTimeString,
                            Duration = 3,
                            Asserts = 4,
                            Label = "f",
                            Reason = new TestReason
                            {
                                Message = "g",
                            },
                            Failure = new TestFailure
                            {
                                Message = "h",
                                StackTrace = "i",
                            },
                        },
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Result = TestResult.Skipped,
                            Seed = 2,
                            StartTime = DateTimeString,
                            EndTime = DateTimeString,
                            Duration = 3,
                            Asserts = 4,
                            Label = "f",
                            Reason = new TestReason
                            {
                                Message = "g",
                            },
                            Failure = new TestFailure
                            {
                                Message = "h",
                                StackTrace = "i",
                            },
                        },
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Result = TestResult.Failed,
                            Seed = 2,
                            StartTime = DateTimeString,
                            EndTime = DateTimeString,
                            Duration = 3,
                            Asserts = 4,
                            Label = "f",
                            Reason = new TestReason
                            {
                                Message = "g",
                            },
                            Failure = new TestFailure
                            {
                                Message = "h",
                                StackTrace = "i",
                            },
                        },
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Result = TestResult.Error,
                            Seed = 2,
                            StartTime = DateTimeString,
                            EndTime = DateTimeString,
                            Duration = 3,
                            Asserts = 4,
                            Label = "f",
                            Reason = new TestReason
                            {
                                Message = "g",
                            },
                            Failure = new TestFailure
                            {
                                Message = "h",
                                StackTrace = "i",
                            },
                        },
                    ],
                },
            ],
        };

        // Act
        var result = _testRunMapper.MapTestRunToDto(testRun);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result.StartTime, Is.EqualTo(expectedDateTime));
            Assert.That(result.EndTime, Is.EqualTo(expectedDateTime));
            Assert.That(result.Duration, Is.EqualTo(TimeSpan.FromSeconds(1)));

            Assert.That(result.Environment, Is.Not.Null);
            Assert.That(result.Environment.User, Is.EqualTo("b"));
            Assert.That(result.Environment.MachineName, Is.EqualTo("c"));

            Assert.That(result.Total, Is.EqualTo(6));

            Assert.That(result.Passed, Is.EqualTo(1));
            Assert.That(result.Inconclusive, Is.EqualTo(1));
            Assert.That(result.Failed, Is.EqualTo(1));
            Assert.That(result.Warning, Is.EqualTo(1));
            Assert.That(result.Skipped, Is.EqualTo(1));
            Assert.That(result.Error, Is.EqualTo(1));

            Assert.That(result.TestMinerStatus, Is.EqualTo(TestMinerStatus.Processing));

            Assert.That(result.CalculateMd5Hash(), Is.EqualTo("4C5B92CF1B29105CBD3DD843177E7329"));

            Assert.That(result.Tests, Has.Count.EqualTo(6));

            Assert.That(result.Tests[0].Name, Is.EqualTo("d"));
            Assert.That(result.Tests[0].ClassName, Is.EqualTo("e"));
            Assert.That(result.Tests[0].Result, Is.EqualTo(Result.Passed));
            Assert.That(result.Tests[0].Seed, Is.EqualTo(2));
            Assert.That(result.Tests[0].StartTime, Is.EqualTo(expectedDateTime));
            Assert.That(result.Tests[0].EndTime, Is.EqualTo(expectedDateTime));
            Assert.That(result.Tests[0].Duration, Is.EqualTo(TimeSpan.FromSeconds(3)));
            Assert.That(result.Tests[0].Asserts, Is.EqualTo(4));
            Assert.That(result.Tests[0].Label, Is.EqualTo("f"));
            Assert.That(result.Tests[0].Reason, Is.EqualTo("g"));
            Assert.That(result.Tests[0].FailureMessage, Is.EqualTo("h"));
            Assert.That(result.Tests[0].StackTrace, Is.EqualTo("i"));

            Assert.That(result.Tests[1].Result, Is.EqualTo(Result.Inconclusive));
            Assert.That(result.Tests[2].Result, Is.EqualTo(Result.Warning));
            Assert.That(result.Tests[3].Result, Is.EqualTo(Result.Skipped));
            Assert.That(result.Tests[4].Result, Is.EqualTo(Result.Failed));
        }
    }

    [Test]
    public void MapTestRunToDto_TestRunWithInvalidStartTime_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var testRun = new TestRun
        {
            EndTime = DateTimeString,
            Duration = 1,
            TestSuites = [
                new ()
                {
                    Name = "a",
                    Type = TestSuiteType.Assembly,
                    Environment = new TestEnvironment
                    {
                        User = "b",
                        MachineName = "c",
                    },
                },
            ],
        };

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _testRunMapper.MapTestRunToDto(testRun));

            Assert.That(ex?.ParamName, Is.EqualTo("startTime"));
            Assert.That(ex?.Message, Does.Contain("startTime ('01/01/0001 00:00:00') must not be equal to '01/01/0001 00:00:00'. (Parameter 'startTime')"));
            _mockLogWrapper.Verify(x => x.Error(It.IsAny<ArgumentOutOfRangeException>(), "Failed to map Test Run."), Times.Once);
        }
    }

    [Test]
    public void MapTestRunToDto_TestRunWithInvalidEndTime_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var testRun = new TestRun
        {
            StartTime = DateTimeString,
            Duration = 1,
            TestSuites = [
                new ()
                {
                    Name = "a",
                    Type = TestSuiteType.Assembly,
                    Environment = new TestEnvironment
                    {
                        User = "b",
                        MachineName = "c",
                    },
                },
            ],
        };

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _testRunMapper.MapTestRunToDto(testRun));

            Assert.That(ex?.ParamName, Is.EqualTo("endTime"));
            Assert.That(ex?.Message, Does.Contain("endTime ('01/01/0001 00:00:00') must not be equal to '01/01/0001 00:00:00'. (Parameter 'endTime')"));
            _mockLogWrapper.Verify(x => x.Error(It.IsAny<ArgumentOutOfRangeException>(), "Failed to map Test Run."), Times.Once);
        }
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void MapTestRunToDto_InvalidLabel_ReturnsTestDtoWithStringEmptyLabel(string? label)
    {
        // Arrange
        var testRun = new TestRun
        {
            StartTime = DateTimeString,
            EndTime = DateTimeString,
            TestSuites = [
                new ()
                {
                    Name = "a",
                    Type = TestSuiteType.Assembly,
                    Environment = new TestEnvironment
                    {
                        User = "b",
                        MachineName = "c",
                    },
                    Tests = [
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Label = label,
                        },
                    ],
                },
            ],
        };

        // Act
        var result = _testRunMapper.MapTestRunToDto(testRun);

        // Assert
        Assert.That(result.Tests[0].Label, Is.EqualTo(string.Empty));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void MapTestRunToDto_InvalidReason_ReturnsTestDtoWithStringEmptyReason(string? message)
    {
        // Arrange
        var testRun = new TestRun
        {
            StartTime = DateTimeString,
            EndTime = DateTimeString,
            TestSuites = [
                new ()
                {
                    Name = "a",
                    Type = TestSuiteType.Assembly,
                    Environment = new TestEnvironment
                    {
                        User = "b",
                        MachineName = "c",
                    },
                    Tests = [
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Reason = new TestReason()
                            {
                                Message = message,
                            },
                        },
                    ],
                },
            ],
        };

        // Act
        var result = _testRunMapper.MapTestRunToDto(testRun);

        // Assert
        Assert.That(result.Tests[0].Reason, Is.EqualTo(string.Empty));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void MapTestRunToDto_InvalidFailureMessage_ReturnsTestDtoWithStringEmptyFailureMessage(string? messages)
    {
        // Arrange
        var testRun = new TestRun
        {
            StartTime = DateTimeString,
            EndTime = DateTimeString,
            TestSuites = [
                new ()
                {
                    Name = "a",
                    Type = TestSuiteType.Assembly,
                    Environment = new TestEnvironment
                    {
                        User = "b",
                        MachineName = "c",
                    },
                    Tests = [
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Failure = new TestFailure()
                            {
                                Message = messages,
                            },
                        },
                    ],
                },
            ],
        };

        // Act
        var result = _testRunMapper.MapTestRunToDto(testRun);

        // Assert
        Assert.That(result.Tests[0].FailureMessage, Is.EqualTo(string.Empty));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void MapTestRunToDto_InvalidFailureStackTrace_ReturnsTestDtoWithStringEmptyStackTrace(string? stackTrace)
    {
        // Arrange
        var testRun = new TestRun
        {
            StartTime = DateTimeString,
            EndTime = DateTimeString,
            TestSuites = [
                new ()
                {
                    Name = "a",
                    Type = TestSuiteType.Assembly,
                    Environment = new TestEnvironment
                    {
                        User = "b",
                        MachineName = "c",
                    },
                    Tests = [
                        new TestCase
                        {
                            Name = "d",
                            ClassName = "e",
                            Failure = new TestFailure()
                            {
                                StackTrace = stackTrace,
                            },
                        },
                    ],
                },
            ],
        };

        // Act
        var result = _testRunMapper.MapTestRunToDto(testRun);

        // Assert
        Assert.That(result.Tests[0].StackTrace, Is.EqualTo(string.Empty));
    }

    [Test]
    public void MapTestRunToDto_NoAssemblyTestSuite_ReturnsTestRunDtoWithDefaultEnvironmentDto()
    {
        // Arrange
        var expectedDateTime = DateTime.ParseExact(
            DateTimeString,
            "yyyy-MM-ddTHH:mm:ssK",
            CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal);
        var testRun = new TestRun
        {
            StartTime = DateTimeString,
            EndTime = DateTimeString,
            TestSuites = [
                new ()
                {
                    Name = "a",
                    Tests = [],
                },
            ],
        };

        // Act
        var result = _testRunMapper.MapTestRunToDto(testRun);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result.Environment, Is.Not.Null);
            Assert.That(result.Environment.User, Is.EqualTo("Unknown"));
            Assert.That(result.Environment.MachineName, Is.EqualTo("Unknown"));
            _mockLogWrapper.Verify(x => x.Error(It.IsAny<InvalidOperationException>(), "Failed to extract Test Environment configuration."), Times.Once);
        }
    }

    [Test]
    public void MapTestRunToDto_NullEnvironment_ReturnsTestRunDtoWithDefaultEnvironmentDto()
    {
        // Arrange
        var testRun = new TestRun
        {
            StartTime = DateTimeString,
            EndTime = DateTimeString,
            TestSuites = [
                new ()
                {
                    Name = "a",
                    Type = TestSuiteType.Assembly,
                    Tests = [],
                },
            ],
        };

        // Act
        var result = _testRunMapper.MapTestRunToDto(testRun);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result.Environment, Is.Not.Null);
            Assert.That(result.Environment.User, Is.EqualTo("Unknown"));
            Assert.That(result.Environment.MachineName, Is.EqualTo("Unknown"));
            _mockLogWrapper.Verify(x => x.Warning("Failed to extract Test Environment configuration."), Times.Once);
        }
    }
}
