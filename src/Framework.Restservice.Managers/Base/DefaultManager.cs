using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Codelisk.GeneratorAttributes.WebAttributes.HttpMethod;
using Codelisk.GeneratorAttributes.WebAttributes.Manager;
using Framework.Restservice.Repositories.Base;

namespace Framework.Restservice.Managers.Base
{
    [DefaultManager]
    public class DefaultManager<TDto, TKey, TEntity> : IDefaultManager<TDto, TKey, TEntity> where TDto : class where TEntity : BaseBaseIdDto
    {
        public readonly IDefaultRepository<TEntity, TKey> _repo;
        private readonly IMapper _mapper;

        public DefaultManager(IDefaultRepository<TEntity, TKey> Repo, IMapper mapper)
        {
            _repo = Repo;
            _mapper = mapper;
        }
        [Delete]
        public Task Delete(TKey id)
        {
            return _repo.Delete(id);
        }
        [GetAll]
        public async Task<List<TDto>> GetAll()
        {
            return _mapper.Map<List<TDto>>(await _repo.GetAll());
        }
        [Add]
        public async Task<TDto> Add(TDto t)
        {
            return _mapper.Map<TDto>(await _repo.Add(_mapper.Map<TEntity>(t)));
        }
        [AddRange]
        public async Task AddRange(List<TDto> list)
        {
            await _repo.Add(_mapper.Map<TEntity>(list));
        }
        [Save]
        public async Task<TDto> Save(TDto t)
        {
            return _mapper.Map<TDto>(await _repo.Save(_mapper.Map<TEntity>(t)));
        }
        [Get]
        public async Task<TDto> Get(TKey id)
        {
            return _mapper.Map<TDto>(await _repo.Get(id));
        }

        [GetFull]
        public virtual async Task<object> GetFull(TKey id)
        {
            throw new NotImplementedException();
        }

        [GetAllFull]
        public virtual async Task<List<object>> GetAllFull()
        {
            throw new NotImplementedException();
        }
    }
}
