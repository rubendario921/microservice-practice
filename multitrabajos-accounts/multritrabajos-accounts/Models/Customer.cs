using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multritrabajos_accounts.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCustomer { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        //public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
