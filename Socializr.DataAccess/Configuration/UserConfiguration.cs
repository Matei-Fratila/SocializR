namespace SocializR.DataAccess.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");

        builder
            .HasKey(e => e.Id);

        builder
            .HasAlternateKey(e => e.Email );

        builder.Property(e => e.Id)
            .HasColumnName("Id");

        builder.Property(e => e.BirthDate)
            .HasColumnType("date");

        builder.Property(e => e.CityId)
            .HasColumnName("CityId");

        builder.Property(e => e.CreatedOn)
            .HasColumnType("datetime")
            .HasDefaultValueSql("getdate()");

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(e => e.IsPrivate);

        builder.HasOne(d => d.City)
            .WithMany(p => p.Users)
            .OnDelete(DeleteBehavior.SetNull)
            .HasForeignKey(d => d.CityId)
            .HasConstraintName("FK_Users_Cities");

        builder.HasOne(d => d.ProfilePhoto)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.ProfilePhotoId)
            .HasConstraintName("FK_Users_Photos");
    }
}
