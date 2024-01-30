using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Restservice.Foundation.Dtos.Base;

namespace Framework.Restservice.Repositories.Base
{
    public interface IDefaultUserRepository<TEntity, TKey> : IDefaultRepository<TEntity, TKey> where TEntity : BaseUserDto
    {
    }
}
