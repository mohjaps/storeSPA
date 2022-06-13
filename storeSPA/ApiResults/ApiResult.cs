namespace storeSPA.ApiResults
{
    public class ApiResult
    {
        public ApiResult()
        {
            Errors = new ();
        }
        public Boolean Result { get; set; }
        public List<String>? Errors { get; set; }
    }    
}