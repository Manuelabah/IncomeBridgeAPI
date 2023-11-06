using Newtonsoft.Json;
using WebApplication1.Common;
using WebApplication1.HelperClass.Interface;

namespace WebApplication1.HelperClass.Implementation
{
    public class SendRequest : ISendRequest
    {
        // The helper method to send any request
        public async Task<Response?> SendAsync(HttpRequestMessage httpRequestMessage)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.SendAsync(httpRequestMessage);
                var result = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<Response>(result);
                if(item != null)
                {
                    return new Response
                    {
                        Average_income = item.Average_income,
                        Monthly_income = item.Monthly_income,
                        Yearly_income = item.Yearly_income,
                        Income_source = item.Income_source,

                    };
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }
    }
}
