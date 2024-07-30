using MySql.Data.EntityFramework;
using System.Data.Entity;

namespace FalzoniNetFCSharp.Infra.Data.Context.MySql
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class FalzoniMySqlContext : FalzoniContext
    {
    }
}
