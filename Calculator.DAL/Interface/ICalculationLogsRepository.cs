using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.DAL.Model;

namespace Calculator.DAL.Interface
{
    /// <summary>
    /// Interface class to blueprint the db dapper actions on the calculation logs table
    /// </summary>
    public interface ICalculationLogsRepository
    {
        /// <summary>
        /// Get the correct log entry by the primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CalculatorLog> GetCalculationLogById(int id);

        /// <summary>
        /// Get the correct log entry by IPAddress, useful for returning history of calculations
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
        Task<IEnumerable<CalculatorLog>> GetCalculationLogByIP(string IPAddress);

        /// <summary>
        /// Get all available logs
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CalculatorLog>> GetCalculationLogs();

        /// <summary>
        /// Insert Log into the calculation logs table
        /// </summary>
        /// <param name="calculatorLog"></param>
        /// <returns></returns>
        int InsertCalculationLog(CalculatorLog calculatorLog);
    }
}
