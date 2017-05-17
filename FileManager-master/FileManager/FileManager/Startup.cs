using FileManager.DAL.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FileManager.Startup))]
namespace FileManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           // CreateRoles();
        }

        private void CreateRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new FileManagerDbContext()));

            string[] roles = { "Administrator", "User" };

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                    roleManager.Create(new IdentityRole(role));
            }

        }
    }
}
