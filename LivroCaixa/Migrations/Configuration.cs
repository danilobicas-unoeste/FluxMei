namespace LivroCaixa.Migrations
{
    using LivroCaixa.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LivroCaixa.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LivroCaixa.Models.ApplicationDbContext context)
        {
            if (!(context.Users.Any(u => u.UserName == "admin@teste.com.br")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userToInsert = new ApplicationUser
                {
                    UserName = "admin@teste.com.br",
                    PhoneNumber = "18997583789",
                    Nome = "Administrador",
                    Email = "admin@teste.com.br"
                };
                userManager.Create(userToInsert, "123456");

                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    roleManager.Create(role);
                }
                role = roleManager.FindByName("MeiResponsavel");
                if (role == null)
                {
                    role = new IdentityRole("MeiResponsavel");
                    roleManager.Create(role);
                }
                role = roleManager.FindByName("MeiFuncionario");
                if (role == null)
                {
                    role = new IdentityRole("MeiFuncionario");
                    roleManager.Create(role);
                }

                userManager.AddToRole(userToInsert.Id, "Admin");
            }
        }
    }
}
