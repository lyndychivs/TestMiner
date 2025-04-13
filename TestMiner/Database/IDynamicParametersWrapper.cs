namespace TestMiner.Database
{
    using System.Data;

    using Dapper;

    internal interface IDynamicParametersWrapper
    {
        void Add(string name, object? value, DbType dbType);

        void Clear();

        DynamicParameters GetDynamicParameters();
    }
}