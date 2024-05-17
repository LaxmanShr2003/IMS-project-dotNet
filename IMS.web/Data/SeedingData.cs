using Microsoft.AspNetCore.Identity;

namespace IMS.web.Data
{
    public class SeedingData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String [] Roles = { "SUPERADMIN", "ADMIN", "Buyer", "COUNTER", "STORE", "PUBLIC" };
            foreach(String roleName in Roles)
            {
                if(!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
