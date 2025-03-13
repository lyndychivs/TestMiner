namespace TestMiner.Database
{
    using Dapper;

    internal class DynamicParametersWrapper : IDynamicParametersWrapper
    {
        public DynamicParameters GetDynamicParameters()
        {
            return new DynamicParameters();
        }
    }
}