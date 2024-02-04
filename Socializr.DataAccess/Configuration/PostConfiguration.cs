namespace SocializR.DataAccess.Configuration;

internal class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.Property(e => e.Body).IsRequired();

        builder.Property(e => e.CreatedOn).HasColumnType("datetime");

        builder.Property(e => e.Title).HasMaxLength(300);

        builder.HasOne(d => d.User)
            .WithMany(p => p.Posts)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Posts_Users");
    }
}
