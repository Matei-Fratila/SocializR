namespace SocializR.DataAccess.Configuration;

internal class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable("Likes");

        builder.Property(e => e.PostId).HasColumnName("PostId");

        builder.Property(e => e.UserId).HasColumnName("UserId");

        builder.HasOne(d => d.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Likes_Posts");

        builder.HasOne(d => d.User)
            .WithMany(p => p.Likes)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Likes_Users");

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
