namespace SocializR.DataAccess.Configuration;

internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.Property(e => e.Description)
            .IsRequired()
            .IsUnicode();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
