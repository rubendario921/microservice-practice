using multitrabajos_security.Repositories;

namespace multitrabajos_security.Data
{
    public class DbInitial
    {
        public static void Initializer(Datacontext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;
            }
            var rols = new Models.Rols[]
                {
                new Models.Rols{ Id=1,Description="Administracion",Status="A",CreateAdd = DateTime.UtcNow},
                new Models.Rols{ Id=2,Description="Student",Status="A",CreateAdd = DateTime.UtcNow}
                };
            foreach (var item in rols)
            {
                context.Rols.Add(item);
            }
            context.SaveChanges();

            var users = new Models.Users[] {
                new Models.Users{Id=1,
             Name="Ruben Dario",
             LastName="Carrillo Lopez",
             Email="rubendario921@hotmail.com",
             Password="123456",
             PhoneNumber="0984322045",
             Status="A",
             DateAdd=DateTime.UtcNow,
             RolID=1},
                new Models.Users{Id=2,
             Name="Stalin",
             LastName="Mejia",
             Email="smejia@gmail.com",
             Password="123456",
             PhoneNumber="0999999999",
             Status="A",
             DateAdd=DateTime.UtcNow,
             RolID=2},
            };
            foreach (var item in users)
            {
                context.Users.Add(item);
            }
            context.SaveChanges();
        }
    }
}