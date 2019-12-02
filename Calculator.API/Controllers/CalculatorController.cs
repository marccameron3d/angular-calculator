using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Calculator.API.Model;
using Calculator.DAL.Interface;
using Calculator.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Calculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculationLogsRepository _calculationLogsRepository;
        private IHttpContextAccessor _accessor;
        public CalculatorController(ICalculationLogsRepository calculationLogsRepository, IHttpContextAccessor accessor)
        {
            this._calculationLogsRepository = calculationLogsRepository;
            this._accessor = accessor;
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Calculation> Post([FromBody] CalculatorLog calculatorLog)
        {
            //Retrieving the remote ip, ::1 is a IPv6 fallback for localhost
            var remoteIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            calculatorLog.Timestamp = DateTime.UtcNow;
            calculatorLog.IPAddress = remoteIp == "::1" ? "localhost" : remoteIp;

            var result = this._calculationLogsRepository.InsertCalculationLog(calculatorLog);
            
            if (result > 0)
            {
                return StatusCode((int)HttpStatusCode.OK, new Calculation(){Message = "successfully added to table"});
            }

            //not really unauthorized but an example of error handling.
            return StatusCode((int) HttpStatusCode.Unauthorized,
                new Calculation() {Message = "unable to add to table", StatusCode = HttpStatusCode.Unauthorized});
        }

        /// <summary>
        /// Get all records available.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calculation>>> Get()
        {
            var calculationLogs = await this._calculationLogsRepository.GetCalculationLogs();

            var calculations = new Calculation()
            {
               Response = calculationLogs,
               Message = "Retrieved Logs"
            };

            if (calculationLogs.Any())
            {
                return StatusCode((int) calculations.StatusCode, calculations);
            }

            calculations.Message = "No results found";
            return StatusCode((int)HttpStatusCode.OK, calculations);
        }
    }
}
