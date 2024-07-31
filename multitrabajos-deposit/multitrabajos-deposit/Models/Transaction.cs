using System.ComponentModel.DataAnnotations.Schema;

namespace multitrabajos_deposit.Models
{
    [Table("transaction")]
    public class Transaction
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("type")]
        public string Type { get; set; } = null!;
        [Column("creationdate")]
        public string CreationDate { get; set; } = null!;
        [Column("accountid")]
        public int AccountId { get; set; }
    }
}
