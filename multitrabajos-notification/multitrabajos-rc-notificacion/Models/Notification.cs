using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multitrabajos_rc_notificacion.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Type { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public required decimal Ammount { get; set; }
        public int IdCustomer { get; set; }
        public int IdAccount { get; set; }
    }
}
