namespace TestMiner.Models.Tests.TestRun;

using System;

using NUnit.Framework;

using TestMiner.Models.TestRun;

[TestFixture]
public class TestRunDtoConstructorTests
{
    [Test]
    public void Constructor_WithValidParameters_SetsProperties()
    {
        var startTime = new DateTime(1, DateTimeKind.Utc);
        var endTime = new DateTime(2, DateTimeKind.Utc);
        var duration = TimeSpan.FromSeconds(3);
        var environment = new EnvironmentDto();

        var testRunDto = new TestRunDto(startTime, endTime, duration, environment);

        Assert.Multiple(() =>
        {
            Assert.That(testRunDto.StartTime, Is.EqualTo(startTime));
            Assert.That(testRunDto.EndTime, Is.EqualTo(endTime));
            Assert.That(testRunDto.Duration, Is.EqualTo(duration));
            Assert.That(testRunDto.Environment, Is.EqualTo(environment));
        });
    }

    [Test]
    public void Constructor_WithMinStartTime_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => new TestRunDto(DateTime.MinValue, new DateTime(1, DateTimeKind.Utc), TimeSpan.FromSeconds(0), new EnvironmentDto()));

            Assert.That(ex?.ParamName, Is.EqualTo("startTime"));
            Assert.That(ex?.Message, Does.Contain("startTime ('01/01/0001 00:00:00') must not be equal to '01/01/0001 00:00:00'. (Parameter 'startTime'"));
        });
    }

    [Test]
    public void Constructor_WithMinEndTime_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => new TestRunDto(new DateTime(1, DateTimeKind.Utc), DateTime.MinValue, TimeSpan.FromSeconds(0), new EnvironmentDto()));

            Assert.That(ex?.ParamName, Is.EqualTo("endTime"));
            Assert.That(ex?.Message, Does.Contain("endTime ('01/01/0001 00:00:00') must not be equal to '01/01/0001 00:00:00'. (Parameter 'endTime'"));
        });
    }

    [Test]
    public void Constructor_WithNullEnvironment_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => new TestRunDto(new DateTime(1, DateTimeKind.Utc), new DateTime(2, DateTimeKind.Utc), TimeSpan.FromSeconds(0), null!));

            Assert.That(ex?.ParamName, Is.EqualTo("environment"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'environment')"));
        });
    }
}
