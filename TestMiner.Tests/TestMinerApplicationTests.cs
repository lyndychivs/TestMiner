namespace TestMiner.Tests
{
    using System;
    using System.IO;

    using Moq;

    using NUnit.Framework;

    using TestMiner.DataAccessLayer;
    using TestMiner.Logger;
    using TestMiner.Mapping;
    using TestMiner.Models.TestRun;
    using TestMiner.Serializer;
    using TestMiner.TestReports.NUnit3;
    using TestMiner.Utility;

    [TestFixture]
    public class TestMinerApplicationTests
    {
        private readonly Mock<ILogWrapper> _mockLogWrapper = new();

        private readonly Mock<IFileWrapper> _mockFileWrapper = new();

        private readonly Mock<ITestReportSerializer> _mockTestReportSerializer = new();

        private readonly Mock<ITestRunMapper> _mockTestRunMapper = new();

        private readonly Mock<ITestMinerDal> _mockTestMinerDal = new();

        private readonly TestMinerApplication _testMinerApplication;

        public TestMinerApplicationTests()
        {
            _testMinerApplication = new TestMinerApplication(
                _mockLogWrapper.Object,
                _mockFileWrapper.Object,
                _mockTestReportSerializer.Object,
                _mockTestRunMapper.Object,
                _mockTestMinerDal.Object);
        }

        [Test]
        public void ProcessFiles_ValidFilePathOfTestReport_ReturnsZero()
        {
            // Arrange
            var filePath = "report.xml";
            var filePaths = new[] { filePath };
            _mockFileWrapper.Setup(file => file.Exists(filePath)).Returns(true);
            _mockFileWrapper.Setup(file => file.ReadAllText(filePath)).Returns("report content");
            _mockTestReportSerializer.Setup(serializer => serializer.Deserialize("report content")).Returns(new TestRun());
            var mockTestRunDto = new Mock<ITestRunDto>();
            mockTestRunDto.Setup(m => m.CalculateMd5Hash()).Returns("md5hash");
            _mockTestRunMapper.Setup(mapper => mapper.MapTestRunToDto(It.IsAny<TestRun>())).Returns(mockTestRunDto.Object);
            _mockTestMinerDal.Setup(dal => dal.IsTestRunPreviouslyRecorded("md5hash")).Returns(false);

            // Act
            var result = _testMinerApplication.ProcessFiles(filePaths);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockTestMinerDal.Verify(dal => dal.RecordTestRun(It.IsAny<ITestRunDto>()), Times.Once);
            _mockLogWrapper.Verify(log => log.Info($"Finished Processing File with Hash: md5hash - {filePath}"), Times.Once);
        }

        [Test]
        public void ProcessFiles_MultipleValidFilePathOfTestReports_ReturnsZero()
        {
            // Arrange
            var filePath = "report.xml";
            var filePaths = new[] { filePath, filePath };
            _mockFileWrapper.Setup(file => file.Exists(filePath)).Returns(true);
            _mockFileWrapper.Setup(file => file.ReadAllText(filePath)).Returns("report content");
            _mockTestReportSerializer.Setup(serializer => serializer.Deserialize("report content")).Returns(new TestRun());
            var mockTestRunDto = new Mock<ITestRunDto>();
            mockTestRunDto.Setup(m => m.CalculateMd5Hash()).Returns("md5hash");
            _mockTestRunMapper.Setup(mapper => mapper.MapTestRunToDto(It.IsAny<TestRun>())).Returns(mockTestRunDto.Object);
            _mockTestMinerDal.Setup(dal => dal.IsTestRunPreviouslyRecorded("md5hash")).Returns(false);

            // Act
            var result = _testMinerApplication.ProcessFiles(filePaths);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockTestMinerDal.Verify(dal => dal.RecordTestRun(It.IsAny<ITestRunDto>()), Times.Exactly(2));
            _mockLogWrapper.Verify(log => log.Info($"Finished Processing File with Hash: md5hash - {filePath}"), Times.Exactly(2));
        }

        [Test]
        public void ProcessFiles_NullFilePaths_ReturnsOne()
        {
            var result = _testMinerApplication.ProcessFiles(null!);

            Assert.That(result, Is.EqualTo(1));
            _mockLogWrapper.Verify(log => log.Error(It.IsAny<ArgumentNullException>(), "filePaths cannot be null."), Times.Once);
        }

        [Test]
        public void ProcessFiles_EmptyFilePaths_ReturnsZero()
        {
            var filePaths = Array.Empty<string>();

            var result = _testMinerApplication.ProcessFiles(filePaths);

            Assert.That(result, Is.EqualTo(0));
            _mockLogWrapper.Verify(log => log.Warning("No Files to Process."), Times.Once);
        }

        [Test]
        public void ProcessFiles_FilePathDoesNotExist_ReturnsZero()
        {
            var filePaths = new[] { "nonexistentfile.xml" };
            _mockFileWrapper.Setup(file => file.Exists("nonexistentfile.xml")).Returns(false);

            var result = _testMinerApplication.ProcessFiles(filePaths);

            Assert.That(result, Is.EqualTo(0));
            _mockLogWrapper.Verify(log => log.Warning(It.IsAny<FileNotFoundException>(), "No File Exists."), Times.Once);
            _mockFileWrapper.Verify(file => file.ReadAllText(It.IsAny<string>()), Times.Never);
        }

        [TestCase("")]
        [TestCase(" ")]
        public void ProcessFiles_FileContentIsInvalid_ReturnsZero(string? fileContent)
        {
            // Arrange
            var emptyFile = "emptyfile.xml";
            var filePaths = new[] { emptyFile };
            _mockFileWrapper.Setup(file => file.Exists(emptyFile)).Returns(true);
            _mockFileWrapper.Setup(file => file.ReadAllText(emptyFile)).Returns(fileContent!);

            // Act
            var result = _testMinerApplication.ProcessFiles(filePaths);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockLogWrapper.Verify(log => log.Warning(It.IsAny<InvalidDataException>(), $"No Text Found in File. {emptyFile}"), Times.Once);
            _mockTestReportSerializer.Verify(serializer => serializer.Deserialize(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ProcessFiles_FileContentIsNull_ReturnsZero()
        {
            // Arrange
            var nullResponseFile = "null.xml";
            var filePaths = new[] { nullResponseFile };
            _mockFileWrapper.Setup(file => file.Exists(nullResponseFile)).Returns(true);
            _mockFileWrapper.Setup(file => file.ReadAllText(nullResponseFile)).Returns((string)null!);

            // Act
            var result = _testMinerApplication.ProcessFiles(filePaths);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockLogWrapper.Verify(log => log.Warning(It.IsAny<InvalidDataException>(), $"No Text Found in File. {nullResponseFile}"), Times.Once);
            _mockTestReportSerializer.Verify(serializer => serializer.Deserialize(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ProcessFiles_DeserializeThrowsAnException_ReturnsZero()
        {
            // Arrange
            var filePath = "a";
            var filePaths = new[] { filePath };
            _mockFileWrapper.Setup(file => file.Exists(filePath)).Returns(true);
            _mockFileWrapper.Setup(file => file.ReadAllText(filePath)).Returns("b");
            _mockTestReportSerializer.Setup(serializer => serializer.Deserialize("b")).Throws(new Exception());

            // Act
            var result = _testMinerApplication.ProcessFiles(filePaths);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "ProcessFiles Failed."), Times.Once);
        }

        [Test]
        public void ProcessFiles_MapTestRunToDtoThrowsAnException_ReturnsZero()
        {
            // Arrange
            var filePath = "a";
            var filePaths = new[] { filePath };
            _mockFileWrapper.Setup(file => file.Exists(filePath)).Returns(true);
            _mockFileWrapper.Setup(file => file.ReadAllText(filePath)).Returns("b");
            _mockTestReportSerializer.Setup(serializer => serializer.Deserialize("b")).Returns(new TestRun());
            _mockTestRunMapper.Setup(mapper => mapper.MapTestRunToDto(It.IsAny<TestRun>())).Throws(new Exception());

            // Act
            var result = _testMinerApplication.ProcessFiles(filePaths);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "ProcessFiles Failed."), Times.Once);
        }

        [Test]
        public void ProcessFiles_CalculateMd5HashThrowsAnException_ReturnsZero()
        {
            // Arrange
            var filePath = "a";
            var filePaths = new[] { filePath };
            _mockFileWrapper.Setup(file => file.Exists(filePath)).Returns(true);
            _mockFileWrapper.Setup(file => file.ReadAllText(filePath)).Returns("b");
            _mockTestReportSerializer.Setup(serializer => serializer.Deserialize("b")).Returns(new TestRun());

            var mockTestRunDto = new Mock<ITestRunDto>();
            mockTestRunDto.Setup(m => m.CalculateMd5Hash()).Throws(new Exception());
            _mockTestRunMapper.Setup(mapper => mapper.MapTestRunToDto(It.IsAny<TestRun>())).Returns(mockTestRunDto.Object);

            // Act
            var result = _testMinerApplication.ProcessFiles(filePaths);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "ProcessFiles Failed."), Times.Once);
        }

        [Test]
        public void ProcessFiles_TestRunAlreadyExistsInDatabase_ReturnsZero()
        {
            // Arrange
            var filePath = "a";
            var filePaths = new[] { filePath };
            _mockFileWrapper.Setup(file => file.Exists(filePath)).Returns(true);
            _mockFileWrapper.Setup(file => file.ReadAllText(filePath)).Returns("b");
            _mockTestReportSerializer.Setup(serializer => serializer.Deserialize("b")).Returns(new TestRun());
            var mockTestRunDto = new Mock<ITestRunDto>();
            mockTestRunDto.Setup(m => m.CalculateMd5Hash()).Returns("c");
            _mockTestRunMapper.Setup(mapper => mapper.MapTestRunToDto(It.IsAny<TestRun>())).Returns(mockTestRunDto.Object);
            _mockTestMinerDal.Setup(dal => dal.IsTestRunPreviouslyRecorded("c")).Returns(true);

            // Act
            var result = _testMinerApplication.ProcessFiles(filePaths);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockLogWrapper.Verify(log => log.Info($"Test Run already exists in Database. c - {filePath}"), Times.Once);
            _mockTestMinerDal.Verify(dal => dal.RecordTestRun(It.IsAny<ITestRunDto>()), Times.Never);
        }

        [Test]
        public void ProcessFiles_RecordTestRunThrowsAnException_ReturnsZero()
        {
            var filePath = "a";
            var filePaths = new[] { filePath };
            _mockFileWrapper.Setup(file => file.Exists(filePath)).Returns(true);
            _mockFileWrapper.Setup(file => file.ReadAllText(filePath)).Returns("b");
            _mockTestReportSerializer.Setup(serializer => serializer.Deserialize("b")).Returns(new TestRun());
            var mockTestRunDto = new Mock<ITestRunDto>();
            mockTestRunDto.Setup(m => m.CalculateMd5Hash()).Returns("c");
            _mockTestRunMapper.Setup(mapper => mapper.MapTestRunToDto(It.IsAny<TestRun>())).Returns(mockTestRunDto.Object);
            _mockTestMinerDal.Setup(dal => dal.IsTestRunPreviouslyRecorded("c")).Returns(false);
            _mockTestMinerDal.Setup(dal => dal.RecordTestRun(It.IsAny<ITestRunDto>())).Throws(new Exception());

            // Act
            var result = _testMinerApplication.ProcessFiles(filePaths);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "ProcessFiles Failed."), Times.Once);
        }
    }
}