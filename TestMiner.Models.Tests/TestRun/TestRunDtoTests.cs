namespace TestMiner.Models.Tests.TestRun
{
    using System;

    using NUnit.Framework;

    using TestMiner.Models.TestRun;

    [TestFixture]
    public class TestRunDtoTests
    {
        private readonly TestRunDto _testRunDto;

        public TestRunDtoTests()
        {
            _testRunDto = new TestRunDto(
                new DateTime(1, DateTimeKind.Utc),
                new DateTime(2, DateTimeKind.Utc),
                TimeSpan.FromSeconds(3),
                new EnvironmentDto());
        }

        [Test]
        public void AddTest_WithNullTestDto_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => _testRunDto.AddTest(null!));

                Assert.That(ex?.ParamName, Is.EqualTo("testDto"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'testDto')"));
            });
        }

        [Test]
        public void AddTest_WhenResultInconclusive_IncreasesTestCount()
        {
            var testDto = new TestDto
            {
                Name = "a",
                ClassName = "b",
                Result = Result.Inconclusive,
            };

            _testRunDto.AddTest(testDto);

            Assert.Multiple(() =>
            {
                Assert.That(_testRunDto.Total, Is.EqualTo(1));
                Assert.That(_testRunDto.Inconclusive, Is.EqualTo(1));
            });
        }

        [Test]
        public void AddTest_WhenResultPassed_IncreasesTestCount()
        {
            var testDto = new TestDto
            {
                Name = "a",
                ClassName = "b",
                Result = Result.Passed,
            };

            _testRunDto.AddTest(testDto);

            Assert.Multiple(() =>
            {
                Assert.That(_testRunDto.Total, Is.EqualTo(1));
                Assert.That(_testRunDto.Passed, Is.EqualTo(1));
            });
        }

        [Test]
        public void AddTest_WhenResultWarning_IncreasesTestCount()
        {
            var testDto = new TestDto
            {
                Name = "a",
                ClassName = "b",
                Result = Result.Warning,
            };

            _testRunDto.AddTest(testDto);

            Assert.Multiple(() =>
            {
                Assert.That(_testRunDto.Total, Is.EqualTo(1));
                Assert.That(_testRunDto.Warning, Is.EqualTo(1));
            });
        }

        [Test]
        public void AddTest_WhenResultSkipped_IncreasesTestCount()
        {
            var testDto = new TestDto
            {
                Name = "a",
                ClassName = "b",
                Result = Result.Skipped,
            };

            _testRunDto.AddTest(testDto);

            Assert.Multiple(() =>
            {
                Assert.That(_testRunDto.Total, Is.EqualTo(1));
                Assert.That(_testRunDto.Skipped, Is.EqualTo(1));
            });
        }

        [Test]
        public void AddTest_WhenResultFailed_IncreasesTestCount()
        {
            var testDto = new TestDto
            {
                Name = "a",
                ClassName = "b",
                Result = Result.Failed,
            };

            _testRunDto.AddTest(testDto);

            Assert.Multiple(() =>
            {
                Assert.That(_testRunDto.Total, Is.EqualTo(1));
                Assert.That(_testRunDto.Failed, Is.EqualTo(1));
            });
        }

        [Test]
        public void AddTest_WhenResultError_IncreasesTestCount()
        {
            var testDto = new TestDto
            {
                Name = "a",
                ClassName = "b",
                Result = Result.Error,
            };

            _testRunDto.AddTest(testDto);

            Assert.Multiple(() =>
            {
                Assert.That(_testRunDto.Total, Is.EqualTo(1));
                Assert.That(_testRunDto.Error, Is.EqualTo(1));
            });
        }

        [Test]
        public void CalculateMd5Hash_WhenCalled_ReturnsMd5Hash()
        {
            var result = _testRunDto.CalculateMd5Hash();

            Assert.That(result, Is.EqualTo("F0A67D0CA129A6CCC4E3381CADCDCCB6"));
        }

        [Test]
        public void ToString_WhenCalled_ReturnsStringRepresentation()
        {
            var result = _testRunDto.ToString();

            Assert.That(result, Is.EqualTo("Total: 0 Inconclusive: 0 Passed: 0 Warning: 0 Skipped: 0 Failed: 0 Error: 0 StartTime: 01/01/0001 00:00:00 EndTime: 01/01/0001 00:00:00 Duration: 00:00:03 Environment: Unknown@Unknown"));
        }
    }
}