using IncomeBridgeAPI.Service.Implementation.Costomer.Interface;
using IncomeBridgeAPI.Service.Implementation.Costomer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service.Implementation.Costomer.Interface;

namespace WebApplication1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GetAccountIncomeController : ControllerBase
    {
        private readonly IGetAccountIncome _accountIncome;
        private readonly ICreateUser _createUser;
        public GetAccountIncomeController(IGetAccountIncome getAccountIncome, ICreateUser createUser)
        {
            _accountIncome = getAccountIncome;
            _createUser = createUser;
        }

        /// <summary>
        /// Return Customer income
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>
        /// Customer income
        /// </returns>
        /// <remarks>
        /// Sample request
        /// Get/api/GetAccountIncome
        /// </remarks>
        /// <response code = "200" > Return customer income</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
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
                    return Ok(await income);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create customer 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>
        /// Return created successful
        /// </returns>
        /// <remarks>
        /// Sample request
        /// Post/api/GetAccountIncome
        /// </remarks>
        /// <response code = "201" > Return created status code </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> CreateUsre(UserDTO customer)
        {
            try
            {
                var cus = _createUser.RegisterAsync(customer);
                if (cus == null)
                {
                    return BadRequest("Inavlide customer details");
                }
                else
                {
                    return Ok(await cus);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
