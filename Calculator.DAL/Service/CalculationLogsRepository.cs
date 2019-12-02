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
    /// <summary>
    /// Interface class to blueprint the db dapper actions on the calculation logs table
    /// </summary>
    public class CalculationLogsRepository : ICalculationLogsRepository
    {
        private readonly IConfiguration _config;

        public CalculationLogsRepository(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Set up connection string for Dapper
        /// </summary>
        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("CalculationLogs"));

        /// <summary>
        /// Get the correct log entry by the primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the correct log entry by IPAddress, useful for returning history of calculations
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get all available logs
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Insert Log into the calculation logs table
        /// </summary>
        /// <param name="calculationLog"></param>
        /// <returns></returns>
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
