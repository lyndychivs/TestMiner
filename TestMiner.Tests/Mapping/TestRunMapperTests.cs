namespace TestMiner.Tests.Mapping
{
    using System;

    using Moq;

    using NUnit.Framework;

    using TestMiner.Logger;
    using TestMiner.Mapping;
    using TestMiner.Models.TestRun;
    using TestMiner.TestReports.NUnit3;

    [TestFixture]
    public class TestRunMapperTests
    {
        private const string DateTimeString = "01/02/2025 03:04:05";

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
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => _testRunMapper.MapTestRunToDto(null!));

                Assert.That(ex?.ParamName, Is.EqualTo("testRun"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'testRun')"));
            });
        }

        [Test]
        public void MapTestRunToDto_ValidTestRun_ReturnsTestRunDto()
        {
            // Arrange
            var expectedDateTime = DateTime.Parse(DateTimeString);
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
                            },
                        ],
                    },
                ],
            };

            // Act
            var result = _testRunMapper.MapTestRunToDto(testRun);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);

                Assert.That(result.StartTime, Is.EqualTo(expectedDateTime));
                Assert.That(result.EndTime, Is.EqualTo(expectedDateTime));
                Assert.That(result.Duration, Is.EqualTo(TimeSpan.FromSeconds(1)));

                Assert.That(result.Environment, Is.Not.Null);
                Assert.That(result.Environment.User, Is.EqualTo("b"));
                Assert.That(result.Environment.MachineName, Is.EqualTo("c"));

                Assert.That(result.Total, Is.EqualTo(1));
                Assert.That(result.Passed, Is.EqualTo(1));
                Assert.That(result.Inconclusive, Is.EqualTo(0));
                Assert.That(result.Failed, Is.EqualTo(0));
                Assert.That(result.Warning, Is.EqualTo(0));
                Assert.That(result.Skipped, Is.EqualTo(0));
                Assert.That(result.Error, Is.EqualTo(0));

                Assert.That(result.TestMinerStatus, Is.EqualTo(TestMinerStatus.Processing));

                Assert.That(result.CalculateMd5Hash(), Is.EqualTo("37F1694EDC8C1031A0237389724D3CB9"));

                Assert.That(result.Tests, Has.Count.EqualTo(1));
                Assert.That(result.Tests[0].Name, Is.EqualTo("d"));
                Assert.That(result.Tests[0].ClassName, Is.EqualTo("e"));
            });
        }
    }
}