namespace TestMiner.Utility
{
    using System;
    using System.IO;

    using TestMiner.Logger;

    internal class FileWrapper : IFileWrapper
    {
        private readonly ILogWrapper _logWrapper;

        public FileWrapper()
            : this(new LogWrapper(typeof(FileWrapper)))
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
                _logWrapper.Error(exception, "Failed to Read All Text.");
                return string.Empty;
            }
        }
    }
}