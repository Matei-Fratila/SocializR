using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocializR.DataAccess;
using SocializR.DataAccess.Seeds;
using SocializR.DataAccess.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<SocializRContext>(connectionString, options =>
{
    options.EnableRetryOnFailure().CommandTimeout(60);
});

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
})
    .AddEntityFrameworkStores<SocializRContext>();

builder.Services.AddAuthentication("SocializRCookies")
        .AddCookie("SocializRCookies", options =>
        {
            options.AccessDeniedPath = new PathString("/Account/Login");
            options.LoginPath = new PathString("/Account/Login");
        });

builder.Services.AddCurrentUser();

builder.Services.AddScoped<SocializRUnitOfWork>();
builder.Services.AddBusinessLogic(builder.Environment);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddWebOptimizer(false, false);
    //builder.Services.AddWebOptimizer(options =>
    //{
    //    options.MinifyCssFiles("AutoLot.Mvc.styles.css");
    //    options.MinifyCssFiles("css/site.css");
    //    options.MinifyJsFiles("js/site.js");
    //});
}
else
{
    builder.Services.AddWebOptimizer(options =>
    {
        options.MinifyCssFiles("SocializR.Web.styles.css");
        options.MinifyCssFiles("css/site.css");
        options.MinifyJsFiles("js/site.js");
        options.AddJavaScriptBundle("js/validationCode.js",
            "js/validations/validators.js", "js/validations/errorFormatting.js");
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //Initialize the database
    if (app.Configuration.GetValue<bool>("RebuildDatabase"))
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SocializRContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        dbContext.Database.EnsureCreated();
        DBInitializer.ApplySeeds(dbContext, userManager, roleManager);
    }
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();