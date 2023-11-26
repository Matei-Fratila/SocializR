namespace SocializR.DataAccess.Seeds;

public static class SeedRoles
{
    public static void Seed(RoleManager<Role> roleManager)
    {
        if (!roleManager.RoleExistsAsync("Administrator").Result)
        {
            Role role = new Role
            {
                Name = "Administrator",
                Description = "Can do anything"
            };

            var result = roleManager.CreateAsync(role).Result;
        }

        if (!roleManager.RoleExistsAsync("RegularUser").Result)
        {
            Role role = new Role
            {
                Name = "RegularUser",
                Description = "Can perform only basic operations"
            };

            var result = roleManager.CreateAsync(role).Result;
        }
    }
}
