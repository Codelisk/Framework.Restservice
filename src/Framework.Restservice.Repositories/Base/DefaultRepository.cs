
using System;

namespace Framework.Restservice.Repositories.Base
{
    [DefaultRepository]
    public class DefaultRepository<TEntity, TKey> : IDefaultRepository<TEntity, TKey> where TEntity : BaseBaseIdDto
    {
        private readonly BaseDbContext _context;
        public DefaultRepository(BaseDbContext context)
        {
            _context = context;
        }

        [Add]
        public virtual async Task<TEntity> Add(TEntity t)
        {
            EntityEntry<TEntity> result;
            result = await  _context.Set<TEntity>().AddAsync(t);

            await _context.SaveChangesAsync();
            return result.Entity;
        }

        [AddRange]
        public virtual async Task AddRange(List<TEntity> list)
        {
            await _context.Set<TEntity>().AddRangeAsync(list);

            await _context.SaveChangesAsync();
        }

        [Save]
        public virtual async Task<TEntity> Save(TEntity t)
        {
            var foundEntity = await _context.Set<TEntity>().FindAsync(t.GetId());
            EntityEntry<TEntity> result = _context.Entry(foundEntity);
            result.CurrentValues.SetValues(t);
            
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        [Get]
        public virtual async Task<TEntity> Get(TKey id)
        {
            return await EntityByIdAsync(id);
        }
        [GetAll]
        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        [Delete]
        public virtual async Task Delete(TKey id)
        {
            var entity = await EntityByIdAsync(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
        private async Task<TEntity> EntityByIdAsync(TKey id)
        {
            if(id is Guid idGuid)
            {
                return await _context.Set<TEntity>().FindAsync(idGuid);
            }

            throw new KeyNotFoundException();
        }
    }
}
