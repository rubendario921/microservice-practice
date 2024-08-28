using multitrabajos_security.Models;

namespace multitrabajos_security.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public DateTime DateAdd { get; set; }
        public int RolID { get; set; }
        public string? RolName { get; set; }
    }
}
