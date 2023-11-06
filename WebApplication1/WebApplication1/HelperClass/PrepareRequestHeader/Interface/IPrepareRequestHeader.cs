namespace WebApplication1.HelperClass.PrepareRequestHeader.Interface
{
    public interface IPrepareRequestHeader
    {
         HttpRequestMessage PrepareRequestHeaderAsync(string customerId);
    }
}
