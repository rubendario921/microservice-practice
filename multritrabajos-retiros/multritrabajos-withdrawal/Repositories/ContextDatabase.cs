using Microsoft.EntityFrameworkCore;
using multritrabajos_withdrawal.Models;

namespace multritrabajos_withdrawal.Repositories
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options) { }
        public DbSet<WithDrawal> WithDrawals { get; set; }
    }
}
