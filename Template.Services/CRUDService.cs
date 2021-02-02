using AutoMapper;
using System;
using System.Threading.Tasks;
using Template.Database;
using Template.Services;
using Template.Core.Interfaces.Services;
using Template.Core.Interfaces.Repository;

namespace Template.Services
{
    public class CRUDService<TModel, TSearch, TDatabase, TInsert, TUpdate> :
        BaseService<TModel, TSearch, TDatabase>, ICRUDService<TModel, TSearch, TInsert, TUpdate>
        where TDatabase : class
    {
        private readonly IMapper _mapper;
        public CRUDService(IUnitOfWork unitOfWork, IMapper mapper, IUriService uriService) : base(unitOfWork, mapper, uriService)
        {
            _mapper = mapper;
        }
        public virtual async Task<TModel> Insert(TInsert request)
        {
            var entity = _mapper.Map<TDatabase>(request);

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> Update(int id, TUpdate request)
        {
            var entity = await _repository.GetByIdAsync(id);
            
            _mapper.Map(request, entity);

            _repository.Update(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<bool> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            try
            {
                _repository.Remove(entity);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}