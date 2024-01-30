using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Restservice.Managers.Base
{
    public interface IDefaultUserManager<TDto, TKey, TEntity> : IDefaultManager<TDto, TKey, TEntity> where TDto : BaseBaseIdDto where TEntity : BaseBaseIdDto
    {
    }
}
