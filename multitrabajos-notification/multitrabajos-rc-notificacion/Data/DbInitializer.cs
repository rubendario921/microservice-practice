using multitrabajos_rc_notificacion.Repositories;

namespace multitrabajos_rc_notificacion.Data
{
    public class DbInitializer
    {
        public static void Initializer(ContextDatabase context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
