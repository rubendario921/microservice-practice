using Microsoft.EntityFrameworkCore;
using multitrabajos_security.Models;

namespace multitrabajos_security.Repositories
{
    public class Datacontext: DbContext
    {
        public Datacontext() { }
        public Datacontext(DbContextOptions<Datacontext> options) : base(options) { }
        public  DbSet<Rols> Rols { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
