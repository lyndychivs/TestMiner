namespace TestMiner.Database
{
    using Dapper;

    internal interface IDynamicParametersWrapper
    {
        DynamicParameters GetDynamicParameters();
    }
}