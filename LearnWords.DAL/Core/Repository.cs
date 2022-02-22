using LearnWords.DAL.Interfaces;
using LearnWords.DAL.Plugins;
using Microsoft.EntityFrameworkCore;

namespace LearnWords.DAL;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : UniqueWithId
{
    AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(TEntity entity)
    {
        if(entity == null)
            throw new ArgumentNullException($"Adding entity was null {typeof(TEntity)}");
         await _context.AddAsync(entity);
    }
    public TEntity? Get(Guid id)
    {
        return _context.Set<TEntity>().AsNoTracking().FirstOrDefault(entity => entity.Id == id);
    }
    public async Task UpdateAsync(TEntity entity)
    {
        var old_entity = await _context.Set<TEntity>().FindAsync(entity.Id);
        old_entity = entity;
        await _context.SaveChangesAsync();
    }
    public void Update(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"Updating entity was null {typeof(TEntity)}");
        _context.Set<TEntity>().Update(entity);
    }
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null)
            _context.Remove(entity);
        else
            throw new ArgumentNullException($"Deleting entity was null {typeof(TEntity)}");
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
