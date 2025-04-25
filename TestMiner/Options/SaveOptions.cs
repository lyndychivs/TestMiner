namespace TestMiner.Options
{
    using CommandLine;

    [Verb("save", false, ["s", "S"], HelpText = "Saves the Database Connection String.")]
    internal class SaveOptions : ISaveOptions
    {
        [Option('c', "connection", Required = true, HelpText = "\nThe Connection String to the Database.")]
        required public string ConnectionString { get; init; }
    }
}