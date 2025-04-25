namespace TestMiner.Utility
{
    internal interface IFileWrapper
    {
        bool Exists(string filePath);

        string ReadAllText(string filePath);

        void WriteAllText(string filePath, string content);
    }
}