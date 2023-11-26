namespace SocializR.DataAccess.Seeds;

static class SeedLikes
{
    public static void Seed(SocializRContext context)
    {
        if (context.Likes.Any())
        {
            return;
        }

        var posts = context.Posts.Select(p=>new { p.Id, p.UserId }).ToList();
        var likes = new List<Like>();
        var random = new Random();

        foreach (var post in posts)
        {
            var friends = context.Users.Where(u => u.Id.ToString() == post.UserId.ToString()).SelectMany(u => u.FriendsFirstUser.Select(f => f.SecondUserId)).ToList();
            var nrOfFriends = friends.Count();

            if (nrOfFriends == 0)
            {
                continue;
            }

            var nrOfLikes = random.Next(0, nrOfFriends);

            for (var i = 0; i < nrOfLikes; i++)
            {
                if (friends.Any() == false)
                {
                    break;
                }

                nrOfFriends = friends.Count();
                var userId = friends[random.Next(nrOfFriends)];

                likes.Add(new Like
                {
                    UserId = userId,
                    PostId=post.Id
                });

                friends.Remove(userId);
            }

            context.AddRange(likes);
            likes.Clear();
            context.SaveChanges();
        }
    }
}
