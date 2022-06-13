namespace storeSPA.ApiResults
{
    public class DataResult<T> : ApiResult
    {
        public List<T>? Data { get; set; }
    }
}
