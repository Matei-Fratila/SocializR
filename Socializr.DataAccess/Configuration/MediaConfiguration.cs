namespace SocializR.DataAccess.Configuration;

internal class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable("Media");

        builder.Property(e => e.Id).HasColumnName("Id");

        builder.Property(e => e.AlbumId).HasColumnName("AlbumId");

        //builder.Property(e => e.Caption).HasMaxLength(300);

        builder.Property(e => e.FilePath).IsRequired();

        builder.Property(e => e.PostId).HasColumnName("PostId");

        builder.Property(e => e.UserId).HasColumnName("UserId");

        builder.HasOne(d => d.Album)
            .WithMany(p => p.Media)
            .HasForeignKey(d => d.AlbumId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Photos_Albums");

        builder.HasOne(d => d.Post)
            .WithMany(p => p.Media)
            .HasForeignKey(d => d.PostId)
            .HasConstraintName("FK_Media_Posts")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(d => d.User)
            .WithMany(p => p.Media)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Photos_Users");
    }
}
