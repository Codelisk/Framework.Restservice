

namespace Framework.Restservice.Repositories.Base
{
    [DefaultRepository]
    [UserDto]
    public class DefaultUserRepository<TEntity, TKey> : DefaultRepository<TEntity, TKey>, IDefaultUserRepository<TEntity, TKey> where TEntity : BaseUserDto
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly UserManager<UserDto> UserManager;
        public DefaultUserRepository(DefaultRepositoryProvider defaultRepositoryProvider, DbContext dbContext) : base(dbContext)
        {
            HttpContextAccessor = defaultRepositoryProvider.HttpContextAccessor;
            UserManager = defaultRepositoryProvider.UserManager;
        }
        [Add]
        public override async Task<TEntity> Add(TEntity t)
        {
            t.UserId = GetUserIdGuid();
            return await base.Add(t);
        }
        [AddRange]
        public override async Task AddRange(List<TEntity> list)
        {
            foreach (var t in list)
            {
                t.UserId = GetUserIdGuid();
            }

            await base.AddRange(list);
        }
        [Save]
        public override async Task<TEntity> Save(TEntity t)
        {
            t.UserId = GetUserIdGuid();
            return await base.Save(t);
        }
        [Get]
        public override async Task<TEntity> Get(TKey id)
        {
            return await base.Get(id);
        }
        [GetLast]
        public override async Task<TEntity> GetLast()
        {
            try
            {
                return await _context.Set<TEntity>().AsNoTracking().Where(x => x.UserId == GetUserIdGuid()).OrderBy(x => (x as ICreatedAt).CreatedAt).LastOrDefaultAsync();
            }
            catch (System.InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Most likely {typeof(TEntity).FullName} does not inherit {typeof(ICreatedAt).FullName}.", ex);
            }
        }
        public Task<List<TEntity>> GetCompletelyAll()
        {
            return base.GetAll();
        }

        [GetAll]
        public override async Task<List<TEntity>> GetAll()
        {
            var uid = GetUserId();
            var result = await base.GetAll();
            return result.Where(x => x.IsUser(uid)).ToList();
        }
        [Delete]
        public override async Task Delete(TKey id)
        {
            await base.Delete(id);
        }

        private TKey GetUserId()
        {
            string id = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (typeof(Guid) == typeof(TKey))
            {
                object parsedGuid = Guid.Parse(id);
                return (TKey)parsedGuid;
            }
            else
            {
                return (TKey)Convert.ChangeType(id, typeof(TKey));
            }
        }
        private Guid GetUserIdGuid()
        {
            object result = this.GetUserId();
            return (Guid)result;
        }
    }
}
