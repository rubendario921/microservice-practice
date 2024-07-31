using Microsoft.EntityFrameworkCore;
using multitrabajos_deposit.Models;

namespace multitrabajos_deposit.Repositories
{
    public class ContextDatabase: DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase>options): base(options) { }
        public DbSet<Transaction> Transaction { get; set; }

    }
}
