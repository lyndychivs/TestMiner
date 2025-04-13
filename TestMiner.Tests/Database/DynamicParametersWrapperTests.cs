namespace TestMiner.Tests.Database
{
    using System.Data;

    using NUnit.Framework;

    using TestMiner.Database;

    [TestFixture]
    public class DynamicParametersWrapperTests
    {
        [Test]
        public void Add_WithValidParameter_AddsParameterToDynamicParameters()
        {
            // Arrange
            var wrapper = new DynamicParametersWrapper();

            // Act
            wrapper.Add("a", "b", DbType.String);
            var dynamicParameters = wrapper.GetDynamicParameters();

            // Assert
            Assert.That(dynamicParameters.ParameterNames, Does.Contain("a"));
            Assert.That(dynamicParameters.Get<string>("a"), Is.EqualTo("b"));
        }

        [Test]
        public void Clear_WithParameters_ClearsDynamicParameters()
        {
            // Arrange
            var wrapper = new DynamicParametersWrapper();
            wrapper.Add("a", "b", DbType.String);

            // Act
            wrapper.Clear();
            var dynamicParameters = wrapper.GetDynamicParameters();

            // Assert
            Assert.That(dynamicParameters.ParameterNames, Is.Empty);
        }

        [Test]
        public void GetDynamicParameters_WithNoParameters_ReturnsEmptyDynamicParameters()
        {
            // Arrange
            var wrapper = new DynamicParametersWrapper();

            // Act
            var dynamicParameters = wrapper.GetDynamicParameters();

            // Assert
            Assert.That(dynamicParameters.ParameterNames, Is.Empty);
        }
    }
}