#nullable disable
namespace storeSPA.Utls
{
    public static class SharedFunctions
    {
        public static SqlConnection GetConnectionString()
        {
            var _accessor = new HttpContextAccessor();
            IConfiguration configs =  _accessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            return new SqlConnection(configs.GetConnectionString("DefalutConnection"));
        }
        public static async Task CheckData(WebApplication app)
        {
            using(var serv = app.Services.CreateScope())
            {
                ApplicationDBContext db = serv.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                UserManager<AppUser> _userManager = serv.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                SignInManager<AppUser> _signInManager = serv.ServiceProvider.GetRequiredService<SignInManager<AppUser>>();
            
                if (!db.Users.Any())
                {
                    AppUser user = new ()
                    {
                        First_Name = "admin",
                        Last_Name = "admin",
                        UserName = "admin",
                        Country = "PS",
                        Email = "admin@admin.com",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };
                    IdentityResult result = await _userManager.CreateAsync(user, "admin");
                    if (result.Succeeded)
                        await _userManager.AddClaimAsync(user, new Claim("Auth", "Admin"));
                }
            }
        }
    }
}
