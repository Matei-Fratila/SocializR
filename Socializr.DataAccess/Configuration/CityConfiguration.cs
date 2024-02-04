namespace SocializR.DataAccess.Configuration;

internal class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.Property(e => e.CountyId).HasColumnName("CountyId");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode();

        builder.HasOne(d => d.County)
            .WithMany(p => p.Cities)
            .HasForeignKey(d => d.CountyId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Cities_Counties");
    }
}
