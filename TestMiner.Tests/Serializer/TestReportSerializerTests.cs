namespace TestMiner.Tests.Serializer;

using System;

using Moq;

using NUnit.Framework;

using TestMiner.Logger;
using TestMiner.Serializer;
using TestMiner.TestReports.NUnit3;
using TestMiner.Tests.Mapping;

[TestFixture]
public class TestReportSerializerTests
{
    private readonly Mock<ILogWrapper> _mockLogWrapper;

    private readonly TestReportSerializer _testReportSerializer;

    public TestReportSerializerTests()
    {
        _mockLogWrapper = new Mock<ILogWrapper>();

        _testReportSerializer = new TestReportSerializer(_mockLogWrapper.Object);
    }

    [Test]
    public void Deserialize_ValidFileContent_ReturnsTestRun()
    {
        string fileContent = TestRunReport.Nunit3TestReport;

        var result = _testReportSerializer.Deserialize(fileContent);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StartDateTimeUtc, Is.EqualTo(new DateTime(638759310160000000, DateTimeKind.Utc)));
            Assert.That(result.EndDateTimeUtc, Is.EqualTo(new DateTime(638759310170000000, DateTimeKind.Utc)));
            Assert.That(result.DurationTimeSpan, Is.EqualTo(TimeSpan.FromSeconds(1.271509)));
            Assert.That(result.TestSuites, Has.Count.EqualTo(1));

            Assert.That(result.TestSuites[0].Name, Is.EqualTo("Name.dll"));
            Assert.That(result.TestSuites[0].Type, Is.EqualTo(TestSuiteType.Assembly));
            Assert.That(result.TestSuites[0].Tests, Has.Count.EqualTo(1));

            Assert.That(result.TestSuites[0].Tests[0].Name, Is.EqualTo("Name"));
            Assert.That(result.TestSuites[0].Tests[0].Result, Is.EqualTo(TestResult.Passed));
            Assert.That(result.TestSuites[0].Tests[0].StartDateTimeUtc, Is.EqualTo(new DateTime(638759310163225843, DateTimeKind.Utc)));
            Assert.That(result.TestSuites[0].Tests[0].EndDateTimeUtc, Is.EqualTo(new DateTime(638759310173855632, DateTimeKind.Utc)));
            Assert.That(result.TestSuites[0].Tests[0].DurationTimeSpan, Is.EqualTo(TimeSpan.FromSeconds(1.0629789)));
            Assert.That(result.TestSuites[0].Tests[index: 0].Asserts, Is.EqualTo(1));
            Assert.That(result.TestSuites[0].Tests[index: 0].Reason, Is.Null);
            Assert.That(result.TestSuites[0].Tests[index: 0].Failure, Is.Null);
        }
    }

    [Test]
    public void Deserialize_InvalidContent_ThrowsInvalidOperationException()
    {
        string fileContent = TestRunReport.InvalidNunit3TestReport;

        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _testReportSerializer.Deserialize(fileContent));

            Assert.That(ex?.Message, Is.EqualTo("There is an error in XML document (2, 2)."));
            _mockLogWrapper.Verify(x => x.Error(It.IsAny<Exception>(), "Failed to Deserialize TestRun."), Times.Once);
        }
    }

    [TestCase("")]
    [TestCase(" ")]
    public void Deserialize_InvalidFileContent_ThrowsArgumentException(string? fileContent)
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentException>(() => _testReportSerializer.Deserialize(fileContent!));

            Assert.That(ex?.ParamName, Is.EqualTo("fileContent"));
            Assert.That(ex?.Message, Does.Contain("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'fileContent')"));
        }
    }

    [Test]
    public void Deserialize_NullFileContent_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _testReportSerializer.Deserialize(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("fileContent"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'fileContent')"));
        }
    }
}
