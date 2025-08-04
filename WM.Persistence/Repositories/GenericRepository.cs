using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class GenericRepository<T>(WmDbContext dbContext) : IGenericRepository<T> where T : BaseEntity, new()
{
    protected readonly WmDbContext _dbContext = dbContext;

    public virtual async Task<T> Add(T entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }


    public async virtual Task Delete(long id)
    {
        var entity = new T() { Id = id };
        var entryObj = _dbContext.Entry(entity);
        if (entryObj != null)
        {
            entryObj.State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> Exists(long id)
    {
        var entity = await Get(id);
        return entity != null;
    }

    public virtual async Task<T?> Get(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<List<T>> GetAll()
    {
        var entities = await _dbContext.Set<T>()
            .ToListAsync();
        return entities;
    }

    public virtual async Task Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}
