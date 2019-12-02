using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Calculator.DAL.Interface;
using Calculator.DAL.Model;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Calculator.DAL.Service
{
    public class CalculationLogsRepository : ICalculationLogsRepository
    {
        private readonly IConfiguration _config;

        public CalculationLogsRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("CalculationLogs"));

        public async Task<CalculationLog> GetCalculationLogById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * WHERE ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<CalculationLog>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<CalculationLog>> GetCalculationLogByIP(string IPAddress)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * WHERE IP = @IP";
                conn.Open();
                var result = await conn.QueryAsync<CalculationLog>(sQuery, new {IP = IPAddress});
                return result;
            }
        }

        public async Task<IEnumerable<CalculationLog>> GetCalculationLogs()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM CalculationLogs";
                conn.Open();
                var result = await conn.QueryAsync<CalculationLog>(sQuery);
                return result;
            }
        }

        public int InsertCalculationLog(CalculationLog calculationLog)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "INSERT INTO CalculationLogs (CalculationLog) VALUES (@CalculationLog)";
                conn.Open();
                var affectedRows = conn.Execute(sQuery, calculationLog);
                return affectedRows;
            }
        }
    }
}
