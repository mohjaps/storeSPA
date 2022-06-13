namespace storeSPA.ApiResults
{
    public class LoginResult : ApiResult
    {
        public String? Token { get; set; }
        public String? Email { get; set; }
        public String? Name { get; set; }
        public String? Id { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
