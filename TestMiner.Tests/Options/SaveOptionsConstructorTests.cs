namespace TestMiner.Tests.Options;

using NUnit.Framework;

using TestMiner.Options;

[TestFixture]
public class SaveOptionsConstructorTests
{
    [Test]
    public void Constructor_WithConnectionString_ReturnsSaveOptions()
    {
        var connectionString = "a";

        var saveOptions = new SaveOptions()
        {
            ConnectionString = connectionString,
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(saveOptions, Is.Not.Null);
            Assert.That(saveOptions.ConnectionString, Is.EqualTo(connectionString));
        }
    }
}
