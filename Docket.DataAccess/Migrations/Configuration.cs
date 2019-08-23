namespace Docket.DataAccess.Migrations
{
    using Docket.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Docket.DataAccess.DocketDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Docket.DataAccess.DocketDbContext context)
        {
            context.Clients.AddOrUpdate(
                f => f.FirstName,
                new Client { FirstName = "Thomas", LastName = "Huber", MiddleInitial="D", Email="test@test.com", BirthDate = DateTime.Now},
                new Client { FirstName = "Urs", LastName = "Meier", MiddleInitial = "A", Email = "test@test.com", BirthDate = DateTime.Now },
                new Client { FirstName = "Erkan", LastName = "Egin", MiddleInitial = "F", Email = "test@test.com", BirthDate = DateTime.Now },
                new Client { FirstName = "Sara", LastName = "Huber", MiddleInitial = "", Email = "test@test.com", BirthDate = DateTime.Now }
                );
            context.ProgrammingLanguages.AddOrUpdate(
    pl => pl.Name,
    new ProgrammingLanguage { Name = "C#" },
    new ProgrammingLanguage { Name = "Typescript" },
    new ProgrammingLanguage { Name = "Jave" },
    new ProgrammingLanguage { Name = "Javascript" },
    new ProgrammingLanguage { Name = "Angular" }
    );
        }
    }
}
