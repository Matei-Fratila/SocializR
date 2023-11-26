namespace SocializR.DataAccess.Configuration;

internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id");

        builder.Property(e => e.Body)
            .IsRequired()
            .IsUnicode();

        builder.Property(e => e.CreatedOn).HasColumnType("datetime");

        builder.Property(e => e.PostId).HasColumnName("PostId");

        builder.Property(e => e.UserId).HasColumnName("UserId");

        builder.HasOne(d => d.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Comments_Posts");

       builder.HasOne(d => d.User)
            .WithMany(p => p.Comments)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Comments_Users");
    }
}
