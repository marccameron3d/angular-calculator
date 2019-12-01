using System.Net;
using Calculator.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Calculation> Post()
        {
            var calc =  new Calculation()
            {
                Response = 100,
                Message = "Post Calculation",
                StatusCode = HttpStatusCode.Accepted,
            };
            return StatusCode((int)calc.StatusCode, calc);
        }

        [HttpGet]
        public ActionResult<Calculation> Get()
        {
            var calc = new Calculation()
            {
                Response = 0,
                Message = "Get Calculation",
            };

            return StatusCode((int)calc.StatusCode, calc);
        }
    }
}
