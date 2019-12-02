using System.Collections.Generic;
using System.Net;
using Calculator.API.Interface;
using Calculator.DAL.Model;

namespace Calculator.API.Model
{
    public class Calculation : IServiceResponse
    {
        public IEnumerable<CalculatorLog> Response { get; set; } = new List<CalculatorLog>();
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; }
    }
}
