using System.Threading.Tasks;
using Careers.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Careers.Models.RolesInitializer
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, string adminEmail, string password,string phone)
        {
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if (!await roleManager.RoleExistsAsync("specialist"))
            {
                await roleManager.CreateAsync(new IdentityRole("specialist"));
            }

            if (!await roleManager.RoleExistsAsync("client"))
            {
                await roleManager.CreateAsync(new IdentityRole("client"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new AppUser
                {
                    Email = adminEmail,
                    PhoneNumber = phone,
                    UserName = adminEmail,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin,"admin" );
                }
            }
        }
    }
}
