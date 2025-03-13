namespace TestMiner.Utility
{
    internal interface IConnectionConfigurationBuilder
    {
        Connection? BuildConnection(string filePath);
    }
}