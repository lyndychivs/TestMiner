namespace TestMiner.Utility
{
    using System;
    using System.IO;

    using TestMiner.Logger;

    internal class FileWrapper : IFileWrapper
    {
        private readonly ILogWrapper _logWrapper;

        public FileWrapper()
            : this(new LogWrapper())
        {
        }

        public FileWrapper(ILogWrapper logWrapper)
        {
            _logWrapper = logWrapper ?? throw new ArgumentNullException(nameof(logWrapper));
        }

        public bool Exists(string filePath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

            try
            {
                return File.Exists(filePath);
            }
            catch
            {
                return false;
            }
        }

        public string ReadAllText(string filePath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception exception)
            {
                _logWrapper.Error(exception, "Failed to read content from File.");

                return string.Empty;
            }
        }

        public void WriteAllText(string filePath, string content)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
            ArgumentException.ThrowIfNullOrWhiteSpace(content);

            try
            {
                File.WriteAllText(filePath, content);
            }
            catch (Exception exception)
            {
                _logWrapper.Error(exception, "Failed to write content to File.");

                throw;
            }
        }
    }
}