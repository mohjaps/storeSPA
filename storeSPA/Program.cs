

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Services
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Perfume Store Api 1",
        Version = "v1",
        Description = "Documantation For Perfume Store Application Project",
        Contact = new OpenApiContact()
        {
            Email = "info@perfumestore.com",
            Name = "Eng. Zakaria AboSilmiyeh - Mohammed Jaber - Sewar Syam",
            Url = new Uri("https://www.perfumestore.com"),
        },
        License = new OpenApiLicense()
        {
            Name = "Perfume Store App Licence",
            Url = new Uri("https://www.perfumestore.com/apilicence")
        }
    });
});
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefalutConnection")));
builder.Services.AddTransient<IUserAuthetication, UserAuthetication>();
builder.Services.AddTransient<IRepository<Perfume>, PerfumeRepo>();

#endregion Services

#region Security
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {

    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(7);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("All", opts => opts.RequireClaim("Auth", new[] {"Admin", "Manager"}));
    options.AddPolicy("Admin", opts => opts.RequireClaim("Auth", "Admin"));
});

var options = builder.Configuration.GetSection("JWT");
var key = options["Key"];

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });
var appSettingsSection = builder.Configuration.GetSection("JWT");
builder.Services.Configure<ApiSettings>(appSettingsSection);
#endregion Security

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PerfumeStore v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

#region Routes
app.MapControllers();
#endregion Routes

await CheckData(app);
app.Run();
