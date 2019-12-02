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
        public async Task<CalculatorLog> GetCalculationLogById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * WHERE ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<CalculatorLog>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        /// <summary>
        /// Get the correct log entry by IPAddress, useful for returning history of calculations
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CalculatorLog>> GetCalculationLogByIP(string IPAddress)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * WHERE IP = @IP";
                conn.Open();
                var result = await conn.QueryAsync<CalculatorLog>(sQuery, new {IP = IPAddress});
                return result;
            }
        }

        /// <summary>
        /// Get all available logs
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CalculatorLog>> GetCalculationLogs()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM CalculatorLogs";
                conn.Open();
                var result = await conn.QueryAsync<CalculatorLog>(sQuery);
                return result;
            }
        }

        /// <summary>
        /// Insert Log into the calculation logs table
        /// </summary>
        /// <param name="calculatorLog"></param>
        /// <returns></returns>
        public int InsertCalculationLog(CalculatorLog calculatorLog)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "INSERT INTO CalculatorLogs (IPAddress,Timestamp,Calculation,Result) VALUES (@IPAddress, @Timestamp, @Calculation, @Result)";
                conn.Open();
                var affectedRows = conn.Execute(sQuery, new 
                {
                    IPAddress = calculatorLog.IPAddress,
                    Timestamp = calculatorLog.Timestamp,
                    Calculation = calculatorLog.Calculation,
                    Result = calculatorLog.Result,
                });
                return affectedRows;
            }
        }
    }
}
