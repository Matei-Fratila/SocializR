namespace SocializR.DataAccess.Configuration;

internal class CountyConfiguration : IEntityTypeConfiguration<County>
{
    public void Configure(EntityTypeBuilder<County> builder)
    {
        builder.ToTable("Counties");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode();

        builder.Property(e => e.ShortName).HasMaxLength(2);
    }
}
