using System.ComponentModel.DataAnnotations;

namespace multitrabajos_security.Models
{
    public class Rols
    {
        [Key]
        public int Id { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public DateTime CreateAdd { get; set; }
        public virtual ICollection<Users> Usuarios { get; set; }
    }
}
