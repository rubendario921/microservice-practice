using multritrabajos_accounts.Repository;

namespace multritrabajos_accounts.Data
{
    public class DataInitializer
    {
        public static void Initialize(ContextDatabase context)
        {
            context.Database.EnsureCreated();

            if (!context.Customer.Any())
            {
                var customers = new Models.Customer[]
                {
                    new Models.Customer{IdCustomer=1,FullName="Ivan Kaviedes",Email="ikaviedes@gmail.com"},
                    new Models.Customer{IdCustomer=2,FullName="Leonel Messi",Email="lmessi@gmail.com"},
                    new Models.Customer{IdCustomer=3,FullName="Stalin Mejía",Email="smejia@gmail.com"},
                    new Models.Customer{IdCustomer=4,FullName="Andrea Pirlo",Email="apirlo@gmail.com"}
                };

                foreach (var customer in customers)
                {
                    context.Customer.Add(customer);
                }
                context.SaveChanges();
            }

            if (!context.Account.Any())
            {
                var accounts = new Models.Account[]
                {
                    new Models.Account{IdAccount=1,TotalAmount=1000,IdCustomer=1},
                    new Models.Account{IdAccount=2,TotalAmount=5000,IdCustomer=2},
                    new Models.Account{IdAccount=3,TotalAmount=2000,IdCustomer=4},
                    new Models.Account{IdAccount=4,TotalAmount=3000,IdCustomer=1},
                    new Models.Account{IdAccount=5,TotalAmount=6000,IdCustomer=3},
                    new Models.Account{IdAccount=6,TotalAmount=500,IdCustomer=3},
                    new Models.Account{IdAccount=7,TotalAmount=800,IdCustomer=1},
                    new Models.Account{IdAccount=8,TotalAmount=100,IdCustomer=4},
                    new Models.Account{IdAccount=9,TotalAmount=20,IdCustomer=2},
                    new Models.Account{IdAccount=10,TotalAmount=1000,IdCustomer=3}
                };

                foreach (var account in accounts)
                {
                    context.Account.Add(account);
                }
                context.SaveChanges();
            }
        }
    }
}
