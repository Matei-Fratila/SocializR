namespace SocializR.DataAccess.Seeds;

static class SeedPosts
{
    public static void Seed(SocializRContext context)
    {
        if (context.Posts.Any())
        {
            return;
        }

        var ids = context.Users.Select(u => u.Id).ToList();
        var nrOfUsers = ids.Count();
        var random = new Random();
        var posts = new List<Post>();

        for (int i = 0; i < 50000; i++)
        {
            var userId = ids[random.Next(nrOfUsers)];
            var date = context.Users.Where(u=>u.Id==userId).Select(u=>u.CreatedOn).FirstOrDefault();
            var dateGenerator = new DateGenerator(date);

            posts.Add(new Post
            {
                Body = "Hello!" + i,
                CreatedOn = dateGenerator.GetRandomDay(),
                UserId = userId
            });
        };

        context.AddRange(posts);
        context.SaveChanges();
    }
}
