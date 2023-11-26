namespace SocializR.DataAccess.Configuration;

internal class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
{
    public void Configure(EntityTypeBuilder<FriendRequest> builder)
    {
        builder.ToTable("FriendRequests");

        builder.HasKey(e => new { e.RequesterUserId, e.RequestedUserId });

        builder.Property(e => e.RequesterUserId).HasColumnName("RequesterUserId");

        builder.Property(e => e.RequestedUserId).HasColumnName("RequestedUserId");

        builder.Property(e => e.CreatedOn).HasColumnType("datetime");

        builder.Property(e => e.RequestMessage)
            .HasMaxLength(500)
            .IsUnicode();

        builder.HasOne(d => d.RequestedUser)
            .WithMany(p => p.FriendRequestsRequestedUser)
            .HasForeignKey(d => d.RequestedUserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Friends_Users1");

        builder.HasOne(d => d.RequesterUser)
            .WithMany(p => p.FriendRequestsRequesterUser)
            .HasForeignKey(d => d.RequesterUserId)
            .HasConstraintName("FK_Friends_Users");
    }
}
