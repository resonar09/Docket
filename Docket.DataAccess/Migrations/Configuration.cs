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
                new Client { FirstName = "Thomas", LastName = "Huber" },
                new Client { FirstName = "Urs", LastName = "Meier" },
                new Client { FirstName = "Erkan", LastName = "Egin" },
                new Client { FirstName = "Sara", LastName = "Huber" }
                );
        }
    }
}
