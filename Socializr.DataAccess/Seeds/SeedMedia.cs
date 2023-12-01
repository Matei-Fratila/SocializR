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
                FilePath = @"images\uploads\blep.jpg",
            },

            new Media
            {
                User = context.Users.FirstOrDefault(),
                Type = MediaTypes.Image,
                FilePath = @"images\uploads\doggo.jpg",
            },

            new Media
            {
                User = context.Users.FirstOrDefault(),
                Type = MediaTypes.Image,
                FilePath = @"images\uploads\pupper.jpg",
            },
        };

        context.AddRange(media);
        context.SaveChanges();
    }
}
