using multitrabajos_security.Models;

namespace multitrabajos_security.DTOs
{
    public class UserRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }        
        public int RolID { get; set; }
        
    }
}
