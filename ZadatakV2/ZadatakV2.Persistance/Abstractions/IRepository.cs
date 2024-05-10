namespace ZadatakV2.Persistance.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetItemByIdAsync(long id);
        Task<IEnumerable<T>> GetItemsAsync();        
        Task AddItemAsync(T entity);
        Task UpdateItemAsync(T entity);
        Task DeleteItemAsync(T entity);
    }
}
