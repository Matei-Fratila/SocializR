﻿namespace SocializR.DataAccess.Configuration;

internal class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("Albums");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.UserId).HasColumnName("UserId");

        builder.HasOne(d => d.User)
            .WithMany(p => p.Albums)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Albums_Users")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
