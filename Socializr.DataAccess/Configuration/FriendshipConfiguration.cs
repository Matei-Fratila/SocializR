namespace SocializR.DataAccess.Configuration;

internal class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.ToTable("Friendships");

        builder.Property(e => e.FirstUserId).HasColumnName("FirstUserId");

        builder.Property(e => e.SecondUserId).HasColumnName("SecondUserId");

        builder.HasOne(d => d.FirstUser)
            .WithMany(p => p.FriendsFirstUser)
            .HasForeignKey(d => d.FirstUserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Friends_Users2");

        builder.HasOne(d => d.SecondUser)
            .WithMany(p => p.FriendsSecondUser)
            .HasForeignKey(d => d.SecondUserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Friends_Users3");

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
