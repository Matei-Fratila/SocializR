using Microsoft.AspNetCore.Identity;
using SocializR.Entities;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Utils;
using System;
using SocializR.Entities.Enums;

namespace SocializR.DataAccess.Seeds
{
    public static class SeedUsers
    {
        public static async Task Seed(UserManager<User> userManager, SocializRContext context)
        {
            if (userManager.Users.Any())
            {
                return;
            }

            var generator = new DateGenerator(new DateTime(2014, 1, 2));
            var random = new Random();
            var cities = context.Cities.Select(c => c.Id).ToList();

            var admin = new User
            {
                CityId = cities[random.Next(0, cities.Count)],
                FirstName = "Matei",
                LastName = "Fratila",
                UserName = "matei.fratila@essensys.ro",
                Email = "matei.fratila@essensys.ro",
                Gender = GenderTypes.Male,
                IsPrivate = true,
                IsDeleted = false,
                IsActive = true,
                BirthDate = Convert.ToDateTime("1997-01-08"),
                CreatedOn = generator.GetRandomDay()
            };

            var result = userManager.CreateAsync(admin, admin.FirstName + admin.LastName + "8").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(admin, "administrator").Wait();
                await userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Email, admin.Email));
            }

            //for (int i = 0; i < 10000; i++)
            //{
            //    var firstname = "alexandru" + i.ToString();
            //    var lastname = "popescu" + i.ToString();

            //    GenderTypes gender;
            //    bool privacy;
            //    gender = (GenderTypes)random.Next(3);
            //    privacy = random.Next(0, 2) == 0 ? false : true;

            //    var user = new User
            //    {
            //        CityId = cities[random.Next(0, cities.Count)],
            //        FirstName = firstname,
            //        LastName = lastname,
            //        UserName = firstname + lastname,
            //        Email = firstname + lastname + "@essensys.ro",
            //        Gender = gender,
            //        IsPrivate = privacy,
            //        IsDeleted = false,
            //        IsActive = true,
            //        BirthDate = Convert.ToDateTime("1997-06-23"),
            //        CreatedOn = generator.GetRandomDay()
            //    };

            //    result = userManager.CreateAsync(user, firstname + lastname + i).Result;

            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(user, "regularuser");
            //        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            //    }
            //}

            return;
        }
    }
}

