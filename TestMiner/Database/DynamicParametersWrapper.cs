namespace TestMiner.Database
{
    using System.Data;

    using Dapper;

    internal class DynamicParametersWrapper : IDynamicParametersWrapper
    {
        private DynamicParameters _dynamicParameters = new ();

        public void Add(string name, object? value, DbType dbType)
        {
            _dynamicParameters.Add(name, value, dbType);
        }

        public void Clear()
        {
            _dynamicParameters = new DynamicParameters();
        }

        public DynamicParameters GetDynamicParameters()
        {
            return _dynamicParameters;
        }
    }
}