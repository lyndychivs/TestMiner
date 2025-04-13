namespace TestMiner.Models.Tests.TestRun
{
    using System;

    using NUnit.Framework;

    using TestMiner.Models.TestRun;

    [TestFixture]
    public class TestDtoTests
    {
        [Test]
        public void ToString_WithValuesSpecified_ReturnsExpectedString()
        {
            var test = new TestDto
            {
                Name = "a",
                ClassName = "b",
                Result = Result.Passed,
                StartTime = DateTime.MinValue,
                EndTime = DateTime.MinValue,
                Duration = TimeSpan.FromSeconds(1),
                Asserts = 2,
                FailureMessage = "c",
                StackTrace = "d",
                Reason = "e",
            };

            var result = test.ToString();

            Assert.That(result, Is.EqualTo("b.a Result: Passed StartTime: 01/01/0001 00:00:00 EndTime: 01/01/0001 00:00:00 Duration: 00:00:01 Asserts: 2 FailureMessage: c"));
        }

        [Test]
        public void ToString_WithOnlyRequiredValuesSpecified_ReturnsDefaultString()
        {
            var test = new TestDto()
            {
                Name = "a",
                ClassName = "b",
            };

            var result = test.ToString();

            Assert.That(result, Is.EqualTo("b.a Result: Inconclusive StartTime: 01/01/0001 00:00:00 EndTime: 01/01/0001 00:00:00 Duration: 00:00:00 Asserts: 0 FailureMessage: "));
        }
    }
}