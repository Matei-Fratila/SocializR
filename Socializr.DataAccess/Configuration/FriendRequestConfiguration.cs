namespace SocializR.DataAccess.Configuration;

internal class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
{
    public void Configure(EntityTypeBuilder<FriendRequest> builder)
    {
        builder.ToTable("FriendRequests");

        builder.Property(e => e.RequesterUserId).HasColumnName("RequesterUserId");

        builder.Property(e => e.RequestedUserId).HasColumnName("RequestedUserId");

        builder.HasOne(d => d.RequestedUser)
            .WithMany(p => p.FriendRequestsRequestedUser)
            .HasForeignKey(d => d.RequestedUserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Friends_Users1");

        builder.HasOne(d => d.RequesterUser)
            .WithMany(p => p.FriendRequestsRequesterUser)
            .HasForeignKey(d => d.RequesterUserId)
            .HasConstraintName("FK_Friends_Users");

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
