using WebApplication1.Common;
using WebApplication1.HelperClass.Interface;
using WebApplication1.HelperClass.PrepareRequestHeader.Interface;
using WebApplication1.Service.Implementation.Costomer.Interface;

namespace WebApplication1.Service.Implementation.Costomer.Implement
{
    public class GetAccountIncome : IGetAccountIncome
    {
        private readonly ISendRequest _sendRequest;
        private readonly IPrepareRequestHeader _prepareRequestHeader;
        public GetAccountIncome(ISendRequest sendRequest, IPrepareRequestHeader prepareRequestHeader) 
        { 
            _sendRequest = sendRequest;
            _prepareRequestHeader = prepareRequestHeader;
        }
        public Task<Response> GetIncomeAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentNullException("CustomerId Field cannot be null");
            }
            var httpRequestMessage = _prepareRequestHeader.PrepareRequestHeaderAsync(customerId);
            var incomeStatus = _sendRequest.SendAsync(httpRequestMessage);
            return incomeStatus;
           
        }
    }
}
