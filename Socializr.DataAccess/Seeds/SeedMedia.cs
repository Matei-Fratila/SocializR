namespace SocializR.DataAccess.Seeds;

static class SeedMedia
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Media.Any())
        {
            return;
        }

        var media = new List < Media >{

            new Media
            {
                User = context.Users.FirstOrDefault(),
                Type = MediaTypes.Image,
                FileName = @"blep.jpg",
            },

            new Media
            {
                User = context.Users.FirstOrDefault(),
                Type = MediaTypes.Image,
                FileName = @"doggo.jpg",
            },

            new Media
            {
                User = context.Users.FirstOrDefault(),
                Type = MediaTypes.Image,
                FileName = @"pupper.jpg",
            },
        };

        context.AddRange(media);
        context.SaveChanges();
    }
}
