namespace WM.Application.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T?> Get(long id);
    Task<List<T>> GetAll();
    Task<T> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task Delete(long id);
}
