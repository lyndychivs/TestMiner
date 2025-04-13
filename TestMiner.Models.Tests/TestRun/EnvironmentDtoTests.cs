namespace TestMiner.Models.Tests.TestRun
{
    using NUnit.Framework;

    using TestMiner.Models.TestRun;

    [TestFixture]
    public class EnvironmentDtoTests
    {
        [TestCase("", "@")]
        [TestCase(" ", " @ ")]
        [TestCase(null, "@")]
        public void ToString_WithInput_ReturnsExpectedResult(string? input, string expectedResult)
        {
            var environment = new EnvironmentDto
            {
                MachineName = input!,
                User = input!,
            };

            var result = environment.ToString();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ToString_WithNoInput_ReturnsDefaultValues()
        {
            var environment = new EnvironmentDto();

            var result = environment.ToString();

            Assert.That(result, Is.EqualTo("Unknown@Unknown"));
        }
    }
}