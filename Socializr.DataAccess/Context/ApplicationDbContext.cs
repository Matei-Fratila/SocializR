namespace SocializR.DataAccess;

public partial class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext()
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    public virtual DbSet<Album> Albums { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<County> Counties { get; set; }
    public virtual DbSet<FriendRequest> FriendRequests { get; set; }
    public virtual DbSet<Friendship> FriendShips { get; set; }
    public virtual DbSet<Interest> Interests { get; set; }
    public virtual DbSet<Like> Likes { get; set; }
    public virtual DbSet<Media> Media { get; set; }
    //public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    //public virtual DbSet<RolePermission> RolePermissions { get; set; }
    //public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<UserInterest> UserInterests { get; set; }
    //public virtual DbSet<UserRole> UserRoles { get; set; }
    //public new virtual DbSet<User> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(@"server=MATEI\\MSSQLSERVER01;initial catalog=SocializR;Integrated Security=true;Encrypt=False;");
    //    optionsBuilder.UseLoggerFactory(new SocializRConsoleLoggerFactory());
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new CountyConfiguration());
        modelBuilder.ApplyConfiguration(new FriendRequestConfiguration());
        modelBuilder.ApplyConfiguration(new FriendshipConfiguration());
        modelBuilder.ApplyConfiguration(new InterestConfiguration());
        modelBuilder.ApplyConfiguration(new LikeConfiguration());
        modelBuilder.ApplyConfiguration(new MediaConfiguration());
        //modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        //modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserInterestConfiguration());
        //modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
