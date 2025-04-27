namespace TestMiner.Logger
{
    using System;

    internal interface ILogWrapper
    {
        void Debug(string message);

        void Error(string message);

        void Error(Exception exception, string message);

        void Info(string message);

        void Warning(string message);

        void Warning(Exception exception, string message);
    }
}