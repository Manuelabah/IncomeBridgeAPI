using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeBridgeAPI.Service.Implementation.Costomer.Model
{
    [Table("Users", Schema = "IncomeBridge")]
    public class User
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
