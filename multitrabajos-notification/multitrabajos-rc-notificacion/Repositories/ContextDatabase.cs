using Microsoft.EntityFrameworkCore;
using multitrabajos_rc_notificacion.Models;

namespace multitrabajos_rc_notificacion.Repositories
{
    public class ContextDatabase: DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options) { }

    public DbSet<Notification> Notification { get; set; }
    }
}
