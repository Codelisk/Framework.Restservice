

namespace Framework.Restservice.Controller.Controllers.Base
{
    [DefaultController]
    [UserDto]
    public class DefaultUserController<T, TKey, TEntity> : DefaultController<T, TKey, TEntity>
        where T : BaseBaseIdDto
        where TEntity : BaseBaseIdDto
        where TKey : IComparable
    {
        protected new readonly IDefaultUserManager<T, TKey, TEntity> _manager;
        public DefaultUserController(IDefaultUserManager<T, TKey, TEntity> manager) : base(manager)
        {
            _manager = manager;
        }

        [AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetCompletelyAll")]
        public Task<List<T>> GetCompletelyAll()
        {
            return _manager.GetCompletelyAll();
        }
    }
}
