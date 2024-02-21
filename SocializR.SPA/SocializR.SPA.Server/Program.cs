using Microsoft.EntityFrameworkCore;
using OwaspHeaders.Core.Extensions;
using SocializR.DataAccess.Seeds;
using SocializR.SPA.Server.ExceptionHandlers;
using System.Net;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<DefaultExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter token",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<ApplicationDbContext>(connectionString, options =>
{
    options.EnableRetryOnFailure().CommandTimeout(60);
});

builder.Services.AddIdentity<User, Role>(options =>
{
    //Password settings
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;

    //Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
        ValidateIssuer = false,//to investigate
        ValidateAudience = false//to investigate
    };
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

if (builder.Environment.IsProduction())
{
    builder.Services.Configure<AzureImageStorageSettings>(builder.Configuration.GetSection(nameof(AzureImageStorageSettings)));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Restricted",
            builder =>
            {
                builder.AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .WithOrigins("https://socializrspaserver.azurewebsites.net")
                .AllowCredentials();
            });
    });
}
builder.Services.AddBusinessLogic(builder.Environment);
builder.Services.AddCurrentUser();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
    options.OnRejected = async (context, token) =>
    {
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", cancellationToken: token);
    };
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.Connection.RemoteIpAddress.ToString(),
        factory: _ => new FixedWindowRateLimiterOptions
        {
            QueueLimit = 10,
            PermitLimit = 50,
            Window = TimeSpan.FromSeconds(15)
        }));
    options.AddPolicy(policyName: "ShortLimit", context =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(context.Connection.RemoteIpAddress.ToString(),
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromSeconds(15)
        });
    });
});

//Scrutor nuget not yet working with .Net 8
//builder.Services.Decorate<ICountyService, CachedCountyService>();

builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
    options.InstanceName = "Demo";
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.Database.Migrate();

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
DBInitializer.ApplySeeds(dbContext, userManager, roleManager);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(opt => { });
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseCors("Restricted");
    //swagger ui not working if security headers are enabled (index.html contains <script> and <styles> tags)
    app.UseSecureHeadersMiddleware();
}

app.UseRateLimiter();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
