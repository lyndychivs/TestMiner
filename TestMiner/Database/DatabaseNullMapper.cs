namespace TestMiner.Database
{
    internal static class DatabaseNullMapper
    {
        public static string? GetNullable(this string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return value;
        }
    }
}