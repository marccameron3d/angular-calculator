using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Calculator.API.Model;
using Calculator.DAL.Interface;
using Calculator.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculationLogsRepository _calculationLogsRepository;

        public CalculatorController(ICalculationLogsRepository calculationLogsRepository)
        {
            this._calculationLogsRepository = calculationLogsRepository;
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Calculation> Post()
        {
            var calcLog = new CalculatorLog()
            {
                IPAddress = "123456789",
                Calculation = "2+2",
                Result = 4,
                Timestamp = DateTime.UtcNow,
            };
            
            var result = this._calculationLogsRepository.InsertCalculationLog(calcLog);
            
            if (result > 0)
            {
                return StatusCode((int)HttpStatusCode.OK, new Calculation(){Message = "successfully added to table"});
            }

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
            };

            if (calculationLogs.Any())
            {
                return StatusCode((int) calculations.StatusCode, calculations.Response);
            }
            
            return StatusCode((int)HttpStatusCode.OK, calculations.Response);
        }
    }
}
