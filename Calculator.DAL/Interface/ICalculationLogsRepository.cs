using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.DAL.Model;

namespace Calculator.DAL.Interface
{
    public interface ICalculationLogsRepository
    {
        Task<CalculationLog> GetCalculationLogById(int id);
        Task<IEnumerable<CalculationLog>> GetCalculationLogByIP(string IPAddress);
        Task<IEnumerable<CalculationLog>> GetCalculationLogs();
        int InsertCalculationLog(CalculationLog calculationLog);
    }
}
