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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Llave primaria
            modelBuilder.Entity<Users>().HasKey(u => u.Id);
            //Relacion 1 a Muchos Users and Rol
            modelBuilder.Entity<Users>().HasOne(u => u.Rol).WithMany(r => r.Users).HasForeignKey(u => u.RolID);

            base.OnModelCreating(modelBuilder);
        }
    }
}