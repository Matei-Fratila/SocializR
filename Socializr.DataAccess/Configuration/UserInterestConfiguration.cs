namespace SocializR.DataAccess.Configuration;

internal class UserInterestConfiguration : IEntityTypeConfiguration<UserInterest>
{
    public void Configure(EntityTypeBuilder<UserInterest> builder)
    {
        builder.ToTable("UserInterests");

        builder.HasKey(e => new { e.UserId, e.InterestId });

        builder.Property(e => e.UserId).HasColumnName("UserId");

        builder.Property(e => e.InterestId).HasColumnName("InterestId");

        builder.HasOne(d => d.Interest)
            .WithMany(p => p.UserInterests)
            .HasForeignKey(d => d.InterestId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_UserInterests_Interests");

        builder.HasOne(d => d.User)
            .WithMany(p => p.UserInterests)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_UserInterests_Users");
    }
}
