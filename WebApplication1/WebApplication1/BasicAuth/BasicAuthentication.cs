using IncomeBridgeAPI.Service.Implementation.Costomer.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace IncomeBridgeAPI.BasicAuth
{
    public class BasicAuthentication : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly DBContext _dbContext;
        public BasicAuthentication(IOptionsMonitor<AuthenticationSchemeOptions> option, ILoggerFactory logger, UrlEncoder code, ISystemClock clock, DBContext dBContext)
            : base(option, logger, code, clock)
        {
            _dbContext = dBContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No header found");
            try
            {

                var haederValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                if (haederValue == null)
                {
                    return AuthenticateResult.Fail("Header value is null");
                }
                var bytes = Convert.FromBase64String(haederValue.Parameter);
                string credentials = Encoding.UTF8.GetString(bytes);
                if (!string.IsNullOrEmpty(credentials))
                {
                    string[] array = credentials.Split(":");
                    string emial = array[0];
                    string password = array[1];
                    var user = await _dbContext.Users.FirstOrDefaultAsync(item => item.Email == emial && item.Password == password);
                    if (user == null)
                        return AuthenticateResult.Fail("UnAuthorized");

                    //Generate Ticket
                    var claims = new[]
                    {
                     new Claim(ClaimTypes.Name, emial)
                 };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("Wrong credential provided");

                }

            }
            catch (Exception)
            {

                return AuthenticateResult.Fail("Invalid Authorization header format");
            }
        }
    }
}
