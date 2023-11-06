using WebApplication1.HelperClass.PrepareRequestHeader.Interface;

namespace WebApplication1.HelperClass.PrepareRequestHeader.Implementation
{
    public class PrepareRequestHeader : IPrepareRequestHeader
    {
        private readonly IConfiguration _configuration;
        public PrepareRequestHeader(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public HttpRequestMessage PrepareRequestHeaderAsync(string customerId)
        {
            var url = _configuration["IncomeUri:uri"];

            var uri = url.Replace("Id", customerId);

            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            httpRequestMessage.Headers.Add(_configuration["MonoConnection:mono-sec-key"], _configuration["MonoConnection:mono-sec-key-value"]);

            return httpRequestMessage;

        }
    }
}
