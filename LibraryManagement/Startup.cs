using LibraryManagement.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryManagement.Startup))]
namespace LibraryManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // creating Creating Admin role     
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new ApplicationRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }

            // In Startup creating SuperAdmin Role and creating a default SuperAdmin User     
            if (!roleManager.RoleExists("SuperAdmin"))
            {

                // first we create Admin role    
                var role = new ApplicationRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);
            }

            //Here we create a SuperAdmin super user who will maintain the website                   


            // creating Creating User role     
            if (!roleManager.RoleExists("User"))
            {
                var role = new ApplicationRole();
                role.Name = "User";
                roleManager.Create(role);

            }


            if (UserManager.FindByEmail("qwerty@gmail.com") == null)
            {
                var user = new ApplicationUser();
                user.FirstName = "Super";
                user.LastName = "Admin";
                user.UserName = "Qwert22";
                user.Email = "qwerty@gmail.com";

                string userPWD = "Qwerty@22";

                var chkUser = UserManager.Create(user, userPWD);

                   
                
            }
            //Add default User to both Role Admin and SuperAdmin 
            if (UserManager.FindByEmail("qwerty@gmail.com") != null)
            {
                var user = UserManager.FindByEmail("qwerty@gmail.com");
                var result1 = UserManager.AddToRole(user.Id, "SuperAdmin");
                UserManager.AddToRole(user.Id, "Admin");

            }

        }
    }
}
