using System.Net;

namespace Calculator.API.Interface
{
    /// <summary>
    /// Small wrapper around API calls to control the status code and message returned
    /// </summary>
    public interface IServiceResponse
    {
        HttpStatusCode StatusCode { get; set; }
        string Message { get; set; }
    }
}
