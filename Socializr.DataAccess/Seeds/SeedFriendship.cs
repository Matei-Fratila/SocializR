namespace SocializR.DataAccess.Seeds;

public static class SeedFriendship
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.FriendShips.Any())
        {
            return;
        }

        var ids = context.Users.Select(u => u.Id).ToList();
        var friendships = new List<Friendship>();
        var randomId = new Random();
        var dateGenerator = new DateGenerator();
        var nrOfUsers = ids.Count();
        var index2 = 0;
        var index1 = 0;

        for (int i = 0; i < 20; i++)
        {
            do
            {
                index1 = randomId.Next(nrOfUsers);
                do
                {
                    index2 = randomId.Next(nrOfUsers);

                } while (index1 == index2);
            }

            while (friendships.Any(f => f.FirstUserId == ids[index1] && f.SecondUserId == ids[index2]));

            var date1 = context.Users.Where(u => u.Id == ids[index1]).Select(u => u.CreatedDate).FirstOrDefault();
            var date2= context.Users.Where(u => u.Id == ids[index2]).Select(u => u.CreatedDate).FirstOrDefault();

            var date = dateGenerator.GetRandomFriendshipDay(date1, date2);

            friendships.Add(new Friendship
            {
                FirstUserId = ids[index1],
                SecondUserId = ids[index2],
                CreatedDate = date
            });

            friendships.Add(new Friendship
            {
                FirstUserId = ids[index2],
                SecondUserId = ids[index1],
                CreatedDate = date
            });
        }

        context.FriendShips.AddRange(friendships);
        context.SaveChanges();
    }
}
