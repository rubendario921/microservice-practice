using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace multitrabajos_security.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public DateTime DateAdd { get; set; }
        public int RolID {  get; set; }
        public virtual Rols Rol { get; set; }
    }
}
