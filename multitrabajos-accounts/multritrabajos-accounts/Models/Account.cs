using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace multritrabajos_accounts.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdAccount { get; set; }
        public decimal TotalAmount { get; set; }
        [ForeignKey("Customer")]
        public int IdCustomer { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
