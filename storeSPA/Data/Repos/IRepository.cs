namespace storeSPA.Data.Repos
{
    public interface IRepository<T> where T : class
    {
        public Task<DataResult<T>> GetById(String Id);
        public Task<DataResult<T>> GetByName(String Name);
        public Task<DataResult<T>> GetAll();
        public Task<DataResult<T>> GetAllForUser(String UserId);
        public Task<ApiResult> Add(T entity);
        public Task<ApiResult> Update(T entity);
        public Task<ApiResult> Delete(String Id);
        public Task<ApiResult> IsExists(String Id);
    }
}
