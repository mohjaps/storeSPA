#nullable disable
namespace storeSPA.Data.Repos
{
    public interface IUserAuthetication
    {
        Task<LoginResult> Authenticate(AuthenticationModel model);
        Task<LoginResult> Register(User register);
        Task<LoginResult> JWTAuth(String Username);
    }

    internal class UserAuthetication : IUserAuthetication
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApiSettings _ApiSettings;

        public UserAuthetication(
            IOptions<ApiSettings> ApiSettings, 
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ApiSettings = ApiSettings.Value;
        }

        public async Task<LoginResult> Authenticate(AuthenticationModel model)
        {
            LoginResult apiResult = new LoginResult();
            try
            {
                AppUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null) 
                {
                    apiResult.Errors.Add("User Does Not Founded");
                    return apiResult;
                }
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (!result.Succeeded)
                {
                    apiResult.Errors.Add("Username Or Password Error");
                    return apiResult;
                }

                LoginResult authUser = await JWTAuth(model.Email);
                return authUser;
            }
            catch (Exception)
            {
                apiResult.Errors.Add("Un Expected Error");
                return apiResult;
            }
        }

        public async Task<LoginResult> JWTAuth(String Username)
        {
            LoginResult apiResult = new LoginResult();

            AppUser user = await _userManager.FindByEmailAsync(Username);

            Claim authClaim = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(x => x.Type == "Auth");


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_ApiSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    authClaim
                }),
                Expires = DateTime.UtcNow.AddDays(_ApiSettings.ExpireDate),
                SigningCredentials = new SigningCredentials
                                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            apiResult.Name = String.Concat(user.First_Name, " ", user.Last_Name);
            apiResult.Email = user.Email;
            apiResult.ExpireDate = DateTime.UtcNow.AddHours(_ApiSettings.ExpireDate);
            apiResult.Id = user.Id;
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            apiResult.Token = tokenHandler.WriteToken(token);
            apiResult.Result = true;
            return apiResult;
        }

        public async Task<LoginResult> Register(User register)
        {
            var uname = register.Email.Split("@")[0];
            LoginResult apiResult = new LoginResult();
            try
            {
                AppUser upass = await _userManager.FindByNameAsync(uname);
                AppUser uemail = await _userManager.FindByEmailAsync(register.Email);
                if (upass is not null || uemail is not null)
                {
                    apiResult.Errors.Add("User Is Already Existed!");
                    return apiResult;
                }

                AppUser user = new AppUser
                {
                    First_Name = register.firstName,
                    Last_Name = register.lastName,
                    UserName = register.Email.Split("@")[0],
                    Email = register.Email,
                };
                IdentityResult result = await _userManager.CreateAsync(user, register.password);
                if (result.Succeeded)
                {
                    IdentityResult claimResult = await _userManager.AddClaimAsync(user, new Claim("Auth", "Manager"));
                    if (!claimResult.Succeeded) throw new Exception();
                    LoginResult userAuth = await JWTAuth(user.Email);
                    return userAuth;
                }
                else
                {
                    apiResult.Errors.Add("Restration Failed!");
                    return apiResult;
                };
            }
            catch (Exception)
            {
                apiResult.Errors.Add("Un Expected Error");
                return apiResult;
            }
        }
    }
}
