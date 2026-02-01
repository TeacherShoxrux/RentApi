using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RentApi.Application.Repositries.Interfaces;
using RentApi.Data;
using RentApi.Data.Entities;

namespace RentApi.Application.Repositries;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
  protected readonly AppDbContext _context;
  internal DbSet<T> dbSet;

  public GenericRepository(AppDbContext context)
  {
    _context = context;
    this.dbSet = _context.Set<T>();
  }

  public async Task<T?> GetByIdAsync(int id)
  {
    return await dbSet.FindAsync(id);
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await dbSet.ToListAsync();
  }

  public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
  {
    return await dbSet.Where(expression).ToListAsync();
  }

  public async Task<T> AddAsync(T entity)
  {
    await dbSet.AddAsync(entity);
    return entity;
  }

  public async Task AddRangeAsync(IEnumerable<T> entities)
  {
    await dbSet.AddRangeAsync(entities);
  }

  public void Remove(T entity)
  {
    if (_context.Entry(entity).State == EntityState.Detached)
    {
      dbSet.Attach(entity);
    }

    dbSet.Remove(entity);
  }

  public void RemoveRange(IEnumerable<T> entities)
  {
    dbSet.RemoveRange(entities);
  }

  public void Update(T entity)
  {
    dbSet.Attach(entity);
    _context.Entry(entity).State = EntityState.Modified;
  }

  // --- ENG KUCHLI FUNKSIYA ---
  public async Task<IEnumerable<T>> GetAllBoxedAsync(
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    string includeProperties = "")
  {
    IQueryable<T> query = dbSet;

    // 1. Filter (Where)
    if (filter != null)
    {
      query = query.Where(filter);
    }

    // 2. Include (Join) - "Equipment,Warehouse" deb vergul bilan berish mumkin
    foreach (var includeProperty in includeProperties.Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    {
      query = query.Include(includeProperty);
    }

    // 3. OrderBy (Saralash)
    if (orderBy != null)
    {
      return await orderBy(query).ToListAsync();
    }
    else
    {
      return await query.ToListAsync();
    }
  }

  public async Task<bool> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync() > 0;
  }
}