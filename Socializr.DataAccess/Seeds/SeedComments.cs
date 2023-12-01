namespace SocializR.DataAccess.Seeds;

static class SeedComments
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Comments.Any())
        {
            return;
        }

        var posts = context.Posts.ToList();
        var comments = new List<Comment>();
        var random = new Random();

        foreach(var post in posts)
        {
            var friends = context.Users.Where(u => u.Id.ToString() == post.UserId.ToString()).SelectMany(u => u.FriendsFirstUser.Select(f=>f.SecondUserId)).ToList();
            var nrOfFriends = friends.Count();

            if (nrOfFriends == 0)
            {
                continue;
            }

            var nrOfComments = random.Next(0, 10);
            var dateGenerator = new DateGenerator(post.CreatedOn);

            for (var i=0; i<nrOfComments; i++)
            {
                try
                {
                    comments.Add(new Comment
                    {
                        UserId = friends[random.Next(nrOfFriends)],
                        Body = "This is such a great post",
                        CreatedOn = dateGenerator.GetRandomDay(),
                        PostId=post.Id
                    });
                }
                catch(Exception e)
                {

                }
            }

            context.Comments.AddRange(comments);
            comments.Clear();
            context.SaveChanges();
        }
    }
}
