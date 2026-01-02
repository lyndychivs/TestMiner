namespace TestMiner.WindowsApplication;

using System;
using System.Windows.Forms;

internal static class Program
{
    [STAThread]
    internal static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new TestMinerForm());
    }
}
