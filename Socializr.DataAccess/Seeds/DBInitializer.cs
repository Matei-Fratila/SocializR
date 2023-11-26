using Microsoft.AspNetCore.Identity;
using SocializR.Entities;
using System.Threading.Tasks;

namespace SocializR.DataAccess.Seeds
{
    public class DBInitializer
    {
        public void ApplySeeds(SocializRContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            SeedCountiesCities.Seed(context);
            SeedInterests.Seed(context);
            SeedRoles.Seed(roleManager);
            SeedUsers.Seed(userManager, context).Wait();
            //SeedFriendship.Seed(context);
            //SeedMedia.Seed(context);
            //SeedPosts.Seed(context);
            //SeedComments.Seed(context);
            //SeedLikes.Seed(context);
        }
    }
}
