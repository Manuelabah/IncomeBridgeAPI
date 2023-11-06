using WebApplication1.Common;

namespace WebApplication1.HelperClass.Interface
{
    public interface ISendRequest
    {
        Task<Response> SendAsync(HttpRequestMessage httpRequestMessage);
    }
}
