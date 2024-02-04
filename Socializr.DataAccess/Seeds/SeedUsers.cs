namespace SocializR.DataAccess.Seeds;

public static class SeedUsers
{
    public static async Task Seed(UserManager<User> userManager, ApplicationDbContext context)
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
            CityId = cities.FirstOrDefault(),
            FirstName = "Matei",
            LastName = "Fratila",
            UserName = "matei.fratila@essensys.ro",
            Email = "matei.fratila@essensys.ro",
            Gender = GenderTypes.Male,
            IsPrivate = true,
            IsDeleted = false,
            IsActive = true,
            BirthDate = Convert.ToDateTime("1997-01-08"),
            CreatedDate = generator.GetRandomDay()
        };

        var result = await userManager.CreateAsync(admin, admin.FirstName + admin.LastName + "8");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "administrator");
            await userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Email, admin.Email));
        }

        for (int i = 0; i < 10; i++)
        {
            var firstname = "alexandru" + i.ToString();
            var lastname = "popescu" + i.ToString();

            GenderTypes gender;
            bool privacy;
            gender = (GenderTypes)random.Next(3);
            privacy = random.Next(0, 2) == 0 ? false : true;

            var user = new User
            {
                CityId = cities[random.Next(0, cities.Count)],
                FirstName = firstname,
                LastName = lastname,
                UserName = firstname + lastname,
                Email = firstname + lastname + "@essensys.ro",
                Gender = gender,
                IsPrivate = privacy,
                IsDeleted = false,
                IsActive = true,
                BirthDate = Convert.ToDateTime("1997-06-23"),
                CreatedDate = generator.GetRandomDay()
            };

            result = await userManager.CreateAsync(user, firstname + lastname.ToUpper() + i);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "regularuser");
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            }
        }

        return;
    }
}

