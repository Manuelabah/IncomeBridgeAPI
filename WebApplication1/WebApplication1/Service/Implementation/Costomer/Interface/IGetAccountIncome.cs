using WebApplication1.Common;

namespace WebApplication1.Service.Implementation.Costomer.Interface
{
    public interface IGetAccountIncome
    {
        Task<Response> GetIncomeAsync(string customerId);
    }
}
