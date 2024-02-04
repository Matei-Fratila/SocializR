namespace SocializR.DataAccess.Configuration;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.Property(e => e.Description).IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
