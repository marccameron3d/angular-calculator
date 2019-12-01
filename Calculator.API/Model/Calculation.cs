using System.Net;
using Calculator.API.Interface;

namespace Calculator.API.Model
{
    public class Calculation : IServiceResponse
    {
        public double Response { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; }
    }
}
