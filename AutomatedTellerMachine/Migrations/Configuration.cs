namespace AutomatedTellerMachine.Migrations
{
    using AutomatedTellerMachine.Models;
    using AutomatedTellerMachine.Services;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomatedTellerMachine.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AutomatedTellerMachine.Models.ApplicationDbContext";
        }

        protected override void Seed(AutomatedTellerMachine.Models.ApplicationDbContext context)
        {
            // Posebne klase za upravljanje korisnicima (korisne metode imaju)
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);


            // Ako u kontekstu ne postoji korisnik sa tim imenom 
                if (!context.Users.Any(t=>t.UserName == "admin@gmail.com"))
                {
                    // Napravi automatski tog korisnika u bazi
                        var user = new ApplicationUser
                        {
                            UserName = "admin@gmail.com",
                            Email = "admin@gmail.com"
                        };
                        userManager.Create(user, "123456Aa@");

                    // Napravi mu tekuci racun putem naseg servisa
                        var service = new TekuciRacunService(context);
                        service.KreirajTekuciRacun("admin", "user", user.Id, 1000);

                // Kreiramo ulogu Administratora
                    context.Roles.AddOrUpdate(r => r.Name, new IdentityRole
                    {
                        Name = "Admin"
                    });

                // Udateamo bazu
                    context.SaveChanges();

                //  Dodjeljujemo korisniku ulogu administratora
                    userManager.AddToRole(user.Id, "Admin");
                }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
