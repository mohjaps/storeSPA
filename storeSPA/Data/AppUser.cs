namespace storeSPA.Data
{
    public class AppUser : IdentityUser
    {
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public String? Country { get; set; }
    }
}
