

namespace Framework.Restservice.Repositories.Base
{
    public interface IDefaultUserRepository<TEntity, TKey> : IDefaultRepository<TEntity, TKey> where TEntity : BaseUserDto
    {
    }
}
