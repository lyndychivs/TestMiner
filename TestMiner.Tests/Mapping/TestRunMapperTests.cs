namespace TestMiner.Tests.Mapping
{
    using System;

    using Moq;

    using NUnit.Framework;

    using TestMiner.Logger;
    using TestMiner.Mapping;
    using TestMiner.TestReports.NUnit3;

    [TestFixture]
    public class TestRunMapperTests
    {
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
            var testRun = new TestRun
            {
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
                            },
                        ],
                    },
                ],
            };

            var result = _testRunMapper.MapTestRunToDto(testRun);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Environment, Is.Not.Null);
                Assert.That(result.Environment.User, Is.EqualTo("b"));
                Assert.That(result.Environment.MachineName, Is.EqualTo("c"));

                Assert.That(result.Total, Is.EqualTo(1));

                Assert.That(result.Tests, Has.Count.EqualTo(1));
                Assert.That(result.Tests[0].Name, Is.EqualTo("d"));
                Assert.That(result.Tests[0].ClassName, Is.EqualTo("e"));
            });
        }
    }
}