namespace TestMiner.Tests;

using System;

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
    public void MineFiles_ValidFilePathOfTestReport_ReturnsZero()
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
        var result = _testMinerApplication.MineFiles(filePaths);

        // Assert
        Assert.That(result, Is.Zero);
        _mockTestMinerDal.Verify(dal => dal.RecordTestRun(It.IsAny<ITestRunDto>()), Times.Once);
        _mockLogWrapper.Verify(log => log.Info($"Finished mining File: md5hash : {filePath}"), Times.Once);
    }

    [Test]
    public void MineFiles_MultipleValidFilePathOfTestReports_ReturnsZero()
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
        var result = _testMinerApplication.MineFiles(filePaths);

        // Assert
        Assert.That(result, Is.Zero);
        _mockTestMinerDal.Verify(dal => dal.RecordTestRun(It.IsAny<ITestRunDto>()), Times.Exactly(2));
        _mockLogWrapper.Verify(log => log.Info($"Finished mining File: md5hash : {filePath}"), Times.Exactly(2));
    }

    [Test]
    public void MineFiles_NullFilePaths_ReturnsOne()
    {
        var result = _testMinerApplication.MineFiles(null!);

        Assert.That(result, Is.EqualTo(1));
        _mockLogWrapper.Verify(log => log.Error("filePaths cannot be null."), Times.Once);
    }

    [Test]
    public void MineFiles_EmptyFilePaths_ReturnsZero()
    {
        var filePaths = Array.Empty<string>();

        var result = _testMinerApplication.MineFiles(filePaths);

        Assert.That(result, Is.Zero);
        _mockLogWrapper.Verify(log => log.Warning("No Files to mine."), Times.Once);
    }

    [Test]
    public void MineFiles_FilePathDoesNotExist_ReturnsOne()
    {
        var filePaths = new[] { "nonexistentfile.xml" };
        _mockFileWrapper.Setup(file => file.Exists("nonexistentfile.xml")).Returns(false);

        var result = _testMinerApplication.MineFiles(filePaths);

        Assert.That(result, Is.EqualTo(1));
        _mockLogWrapper.Verify(log => log.Warning("No File exists: nonexistentfile.xml"), Times.Once);
    }

    [TestCase("")]
    [TestCase(" ")]
    public void MineFiles_FileContentIsInvalid_ReturnsOne(string? fileContent)
    {
        // Arrange
        var emptyFile = "emptyfile.xml";
        var filePaths = new[] { emptyFile };
        _mockFileWrapper.Setup(file => file.Exists(emptyFile)).Returns(true);
        _mockFileWrapper.Setup(file => file.ReadAllText(emptyFile)).Returns(fileContent!);

        // Act
        var result = _testMinerApplication.MineFiles(filePaths);

        // Assert
        Assert.That(result, Is.EqualTo(1));
        _mockLogWrapper.Verify(log => log.Warning($"No Data found in File: {emptyFile}"), Times.Once);
    }

    [Test]
    public void MineFiles_FileContentIsNull_ReturnsOne()
    {
        // Arrange
        var nullResponseFile = "null.xml";
        var filePaths = new[] { nullResponseFile };
        _mockFileWrapper.Setup(file => file.Exists(nullResponseFile)).Returns(true);
        _mockFileWrapper.Setup(file => file.ReadAllText(nullResponseFile)).Returns((string)null!);

        // Act
        var result = _testMinerApplication.MineFiles(filePaths);

        // Assert
        Assert.That(result, Is.EqualTo(1));
        _mockLogWrapper.Verify(log => log.Warning($"No Data found in File: {nullResponseFile}"), Times.Once);
    }

    [Test]
    public void MineFiles_DeserializeThrowsAnException_ReturnsOne()
    {
        // Arrange
        var filePath = "a";
        var filePaths = new[] { filePath };
        _mockFileWrapper.Setup(file => file.Exists(filePath)).Returns(true);
        _mockFileWrapper.Setup(file => file.ReadAllText(filePath)).Returns("b");
        _mockTestReportSerializer.Setup(serializer => serializer.Deserialize("b")).Throws(new Exception());

        // Act
        var result = _testMinerApplication.MineFiles(filePaths);

        // Assert
        Assert.That(result, Is.EqualTo(1));
        _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed to deserialize Test Run from File: a"), Times.Once);
    }

    [Test]
    public void MineFiles_MapTestRunToDtoThrowsAnException_ReturnsOne()
    {
        // Arrange
        var filePath = "a";
        var filePaths = new[] { filePath };
        _mockFileWrapper.Setup(file => file.Exists(filePath)).Returns(true);
        _mockFileWrapper.Setup(file => file.ReadAllText(filePath)).Returns("b");
        _mockTestReportSerializer.Setup(serializer => serializer.Deserialize("b")).Returns(new TestRun());
        _mockTestRunMapper.Setup(mapper => mapper.MapTestRunToDto(It.IsAny<TestRun>())).Throws(new Exception());

        // Act
        var result = _testMinerApplication.MineFiles(filePaths);

        // Assert
        Assert.That(result, Is.EqualTo(1));
        _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed to deserialize Test Run from File: a"), Times.Once);
    }

    [Test]
    public void MineFiles_CalculateMd5HashThrowsAnException_ReturnsOne()
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
        var result = _testMinerApplication.MineFiles(filePaths);

        // Assert
        Assert.That(result, Is.EqualTo(1));
        _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed to mine File: a"), Times.Once);
    }

    [Test]
    public void MineFiles_TestRunAlreadyExistsInDatabase_ReturnsOne()
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
        var result = _testMinerApplication.MineFiles(filePaths);

        // Assert
        Assert.That(result, Is.EqualTo(1));
        _mockLogWrapper.Verify(log => log.Info($"Test Run already exists in Database: c : {filePath}"), Times.Once);
    }

    [Test]
    public void MineFiles_RecordTestRunThrowsAnException_ReturnsOne()
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
        var result = _testMinerApplication.MineFiles(filePaths);

        // Assert
        Assert.That(result, Is.EqualTo(1));
        _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed to mine File: a"), Times.Once);
    }
}
