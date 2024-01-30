
using System;

namespace Framework.Restservice.Repositories.Base
{
    [DefaultRepository]
    public class DefaultRepository<TEntity, TKey> : IDefaultRepository<TEntity, TKey> where TEntity : BaseBaseIdDto
    {
        protected readonly BackendContext _context;
        public DefaultRepository(BackendContext context)
        {
            _context = context;
        }

        [Add]
        public virtual async Task<TEntity> Add(TEntity t)
        {
            EntityEntry<TEntity> result;
            result = await _context.Set<TEntity>().AddAsync(t);

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
        [GetLast]
        public virtual async Task<TEntity> GetLast()
        {
            try
            {
                return await _context.Set<TEntity>().AsNoTracking().OrderBy(x => (x as ICreatedAt).CreatedAt).LastOrDefaultAsync();
            }
            catch (System.InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Most likely {typeof(TEntity).FullName} does not inherit {typeof(ICreatedAt).FullName}.", ex);
            }
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
            if (id is Guid idGuid)
            {
                return await _context.Set<TEntity>().FindAsync(idGuid);
            }

            throw new KeyNotFoundException();
        }
    }
}