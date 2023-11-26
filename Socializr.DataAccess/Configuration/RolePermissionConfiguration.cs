namespace SocializR.DataAccess.Configuration;

internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");

        builder.HasKey(e => new { e.Id, e.PermissionId });

        builder.Property(e => e.Id).HasColumnName("Id");

        builder.Property(e => e.PermissionId).HasColumnName("PermissionId");

        builder.HasOne(d => d.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(d => d.PermissionId)
            .HasConstraintName("FK_RolePermissions_Permissions");

        //builder.HasOne(d => d.Role)
        //    .WithMany(p => p.RolePermissions)
        //    .HasForeignKey(d => d.Id)
        //    .HasConstraintName("FK_RolePermissions_Roles");
    }
}
