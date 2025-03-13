namespace TestMiner.Tests.Database
{
    using NUnit.Framework;

    using TestMiner.Database;

    [TestFixture]
    public class DatabaseNullMapperTests
    {
        [TestCase("", null)]
        [TestCase(" ", null)]
        [TestCase(null, null)]
        [TestCase("a", "a")]
        public void GetNullable_WithValidInput_ReturnsExpected(string? input, string? expected)
        {
            var result = input.GetNullable();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}