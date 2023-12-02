using Npgsql;
using System.Data;
namespace werd.Model
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionstring;
        public DBContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connectionstring = _configuration.GetConnectionString("DBConnection");
        }
        public NpgsqlConnection CreateConnection() => new NpgsqlConnection(connectionstring);
    }
}
