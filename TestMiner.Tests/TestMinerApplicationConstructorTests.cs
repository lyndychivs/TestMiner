namespace TestMiner.Tests
{
    using System;

    using Moq;

    using NUnit.Framework;

    using TestMiner.DataAccessLayer;
    using TestMiner.Logger;
    using TestMiner.Mapping;
    using TestMiner.Serializer;
    using TestMiner.Utility;

    [TestFixture]
    public class TestMinerApplicationConstructorTests
    {
        private readonly Mock<ILogWrapper> _mockLogWrapper = new();

        private readonly Mock<IFileWrapper> _mockFileWrapper = new();

        private readonly Mock<ITestReportSerializer> _mockTestReportSerializer = new();

        private readonly Mock<ITestRunMapper> _mockTestRunMapper = new();

        private readonly Mock<ITestMinerDal> _mockTestMinerDal = new();

        [Test]
        public void Constructor_ValidConnectionString_ReturnsTestMinerApplication()
        {
            var testMinerApplication = new TestMinerApplication(_mockLogWrapper.Object, "Data Source=localhost\\Database=DatabaseName;");

            Assert.That(testMinerApplication, Is.Not.Null);
        }

        [Test]
        public void Constructor_ValidParameters_ReturnsTestMinerApplication()
        {
            var testMinerApplication = new TestMinerApplication(
                _mockLogWrapper.Object,
                _mockFileWrapper.Object,
                _mockTestReportSerializer.Object,
                _mockTestRunMapper.Object,
                _mockTestMinerDal.Object);

            Assert.That(testMinerApplication, Is.Not.Null);
        }

        [Test]
        public void Constructor_NullLogWrapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerApplication(null!, "Data Source=localhost\\Database=DatabaseName;"));

                Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'logWrapper')"));
            });
        }

        [TestCase("")]
        [TestCase(" ")]
        public void Constructor_InvalidConnectionString_ThrowsArgumentException(string? connectionString)
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentException>(() => new TestMinerApplication(_mockLogWrapper.Object, connectionString!));

                Assert.That(ex?.ParamName, Is.EqualTo("connectionString"));
                Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'connectionString')"));
            });
        }

        [Test]
        public void Constructor_NullConnectionString_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerApplication(_mockLogWrapper.Object, null!));

                Assert.That(ex?.ParamName, Is.EqualTo("connectionString"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'connectionString')"));
            });
        }

        [Test]
        public void ConstructorTwo_NullLogWrapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerApplication(
                    null!,
                    _mockFileWrapper.Object,
                    _mockTestReportSerializer.Object,
                    _mockTestRunMapper.Object,
                    _mockTestMinerDal.Object));

                Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'logWrapper')"));
            });
        }

        [Test]
        public void Constructor_NullFileWrapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerApplication(
                    _mockLogWrapper.Object,
                    null!,
                    _mockTestReportSerializer.Object,
                    _mockTestRunMapper.Object,
                    _mockTestMinerDal.Object));

                Assert.That(ex?.ParamName, Is.EqualTo("fileWrapper"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'fileWrapper')"));
            });
        }

        [Test]
        public void Constructor_NullTestReportSerializer_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerApplication(
                    _mockLogWrapper.Object,
                    _mockFileWrapper.Object,
                    null!,
                    _mockTestRunMapper.Object,
                    _mockTestMinerDal.Object));

                Assert.That(ex?.ParamName, Is.EqualTo("testReportSerializer"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'testReportSerializer')"));
            });
        }

        [Test]
        public void Constructor_NullTestRunMapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerApplication(
                    _mockLogWrapper.Object,
                    _mockFileWrapper.Object,
                    _mockTestReportSerializer.Object,
                    null!,
                    _mockTestMinerDal.Object));

                Assert.That(ex?.ParamName, Is.EqualTo("testRunMapper"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'testRunMapper')"));
            });
        }

        [Test]
        public void Constructor_NullTestMinerDal_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerApplication(
                    _mockLogWrapper.Object,
                    _mockFileWrapper.Object,
                    _mockTestReportSerializer.Object,
                    _mockTestRunMapper.Object,
                    null!));

                Assert.That(ex?.ParamName, Is.EqualTo("testMinerDal"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'testMinerDal')"));
            });
        }
    }
}