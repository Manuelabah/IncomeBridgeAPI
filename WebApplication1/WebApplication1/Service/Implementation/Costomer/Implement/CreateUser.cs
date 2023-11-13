using IncomeBridgeAPI.Common;
using IncomeBridgeAPI.Service.Implementation.Costomer.Interface;
using IncomeBridgeAPI.Service.Implementation.Costomer.Model;

namespace IncomeBridgeAPI.Service.Implementation.Costomer.Implement
{
    public class CreateUser : ICreateUser
    {
        private readonly DBContext _dbContext;
        public CreateUser(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserResponse> RegisterAsync(UserDTO registrationRequest)
        {
            var answer = new UserResponse();
            if (registrationRequest != null)
            {
                var user = new User()
                {
                    FirstName = registrationRequest.FirstName,
                    LastName = registrationRequest.LastName,
                    Email = registrationRequest.Email,
                    Password = registrationRequest.Password,

                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                answer = new UserResponse
                {
                    Id = user.Id,
                    Name = $"{user.FirstName + " " + user.LastName}",
                    Password = user.Password,
                    Email = user.Email,
                    Mssage = "Customer created successfully."
                };
                return answer;
            }
            return new UserResponse
            {
                Id = 0,
                Name = null,
                Password = null,
                Email = null,
                Mssage = "Fail to create customer details"
            };

        }
    }
}
