using ctrcprog.api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace ctrcprog.api.Data;
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(object id);
    Task InsertAsync(T obj);
    Task UpdateAsync(T obj);
    Task DeleteAsync(object id);
    Task SaveAsync();
    Task<bool> AnyAsync(Func<T, bool> predicate);

    Task AddRangeAsync(IEnumerable<T> entities);

    Task RemoveRangeAsync(IEnumerable<T> entities);
    DataContext Context { get; }

}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        await Task.CompletedTask; // Since EF Core does not have RemoveRangeAsync, we use Task.CompletedTask
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task InsertAsync(T obj)
    {
        await _dbSet.AddAsync(obj);
    }

    public async Task UpdateAsync(T obj)
    {
        _dbSet.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
        await Task.CompletedTask; // Since EF Core does not have an UpdateAsync, we need to use Task.CompletedTask
    }

    public async Task DeleteAsync(object id)
    {
        T existing = await _dbSet.FindAsync(id);
        if (existing != null)
        {
            _dbSet.Remove(existing);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<bool> AnyAsync(Func<T, bool> predicate)
    {
        return await Task.Run(() => _dbSet.Any(predicate));
    }
    public DataContext Context
    {
        get { return _context; }
    }
}