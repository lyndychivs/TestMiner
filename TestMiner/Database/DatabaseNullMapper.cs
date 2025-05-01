namespace TestMiner.Database
{
    internal static class DatabaseNullMapper
    {
        internal static string? GetNullable(this string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return value;
        }
    }
}