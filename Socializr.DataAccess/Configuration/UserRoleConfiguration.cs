namespace SocializR.DataAccess.Configuration;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(e => new { e.UserId, e.RoleId });

        builder.Property(e => e.UserId).HasColumnName("UserId");

        builder.Property(e => e.RoleId).HasColumnName("RoleId");

        //builder.HasOne(d => d.Role)
        //    .WithMany(p => p.UserRoles)
        //    .HasForeignKey(d => d.RoleId)
        //    .HasConstraintName("FK_UserRoles_Roles");

        //builder.HasOne(d => d.User)
        //    .WithMany(p => p.UserRoles)
        //    .HasForeignKey(d => d.UserId)
        //    .HasConstraintName("FK_UserRoles_Users");
    }
}
