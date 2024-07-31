using multitrabajos_deposit.Repositories;

namespace multitrabajos_deposit.Data
{
    public class DbInitializer
    {
        public static void Initialize(ContextDatabase context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
