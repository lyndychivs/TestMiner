namespace TestMiner.Logger;

using System;

public interface ILogWrapper
{
    void Error(string message);

    void Error(Exception exception, string message);

    void Info(string message);

    void Warning(string message);
}
