using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service.Implementation.Costomer.Interface;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAccountIncomeController : ControllerBase
    {
        private readonly IGetAccountIncome _accountIncome;
        public GetAccountIncomeController(IGetAccountIncome getAccountIncome)
        {
            _accountIncome = getAccountIncome;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountIncome(string customerID)
        {
            try
            {
               var income = _accountIncome.GetIncomeAsync(customerID);
                if (income == null)
                {
                    return NotFound("Inavlide income id passed");
                }
                else
                {
                    return Ok( await income);  
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
