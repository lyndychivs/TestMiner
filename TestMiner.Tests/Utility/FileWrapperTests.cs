namespace TestMiner.Tests.Utility;

using System;

using Moq;

using NUnit.Framework;

using TestMiner.Logger;
using TestMiner.Utility;

[TestFixture]
public class FileWrapperTests
{
    private readonly Mock<ILogWrapper> _mockLogWrapper;

    private readonly FileWrapper _fileWrapper;

    public FileWrapperTests()
    {
        _mockLogWrapper = new Mock<ILogWrapper>();

        _fileWrapper = new FileWrapper(_mockLogWrapper.Object);
    }

    [TestCase("")]
    [TestCase(" ")]
    public void Exists_InvalidFilePath_ThrowsArgumentException(string? filePath)
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentException>(() => _fileWrapper.Exists(filePath!));

            Assert.That(ex?.ParamName, Is.EqualTo("filePath"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'filePath')"));
        }
    }

    [Test]
    public void Exists_NullFilePath_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _fileWrapper.Exists(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("filePath"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'filePath')"));
        }
    }

    [TestCase("")]
    [TestCase(" ")]
    public void ReadAllText_InvalidFilePath_ThrowsArgumentException(string? filePath)
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentException>(() => _fileWrapper.ReadAllText(filePath!));

            Assert.That(ex?.ParamName, Is.EqualTo("filePath"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'filePath')"));
        }
    }

    [Test]
    public void ReadAllText_NullFilePath_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _fileWrapper.ReadAllText(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("filePath"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'filePath')"));
        }
    }

    [TestCase("")]
    [TestCase(" ")]
    public void WriteAllText_InvalidFilePath_ThrowsArgumentException(string filePath)
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentException>(() => _fileWrapper.WriteAllText(filePath, "a"));

            Assert.That(ex?.ParamName, Is.EqualTo("filePath"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'filePath')"));
        }
    }

    [Test]
    public void WriteAllText_NullFilePath_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _fileWrapper.WriteAllText(null!, "a"));

            Assert.That(ex?.ParamName, Is.EqualTo("filePath"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'filePath')"));
        }
    }

    [TestCase("")]
    [TestCase(" ")]
    public void WriteAllText_InvalidContent_ThrowsArgumentException(string content)
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentException>(() => _fileWrapper.WriteAllText("a", content));

            Assert.That(ex?.ParamName, Is.EqualTo("content"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'content')"));
        }
    }

    [Test]
    public void WriteAllText_NullContent_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _fileWrapper.WriteAllText("a", null!));

            Assert.That(ex?.ParamName, Is.EqualTo("content"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'content')"));
        }
    }
}
