using BaseApplication.Dtos;
using BaseApplication.Factory;
using BaseApplication.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace taichu.AbpAiProject
{
    public class AbpAiProjectCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput> : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
        where TGetListInput : IPagedResultRequest
    {
        protected readonly IAppFactory AppFactory;

        public AbpAiProjectCrudAppService(IAppFactory appFactory) : base(appFactory.Repository<TEntity, TKey>())
        {
            AppFactory = appFactory;
        }

        [HttpPost("api/app/[controller]/GetList")]
        public override Task<PagedResultDto<TEntityDto>> GetListAsync(TGetListInput input)
        {
            var queryDto = this.QueryPagedResult(input);
            if (queryDto == null)
            {
                return base.GetListAsync(input);
            }
            return queryDto.GetPagedAsync<TEntityDto>(input.SkipCount, input.MaxResultCount);

        }
        [HttpGet("api/app/[controller]/GetById/{id}")]
        public override Task<TEntityDto> GetAsync(TKey id)
        {
            return base.GetAsync(id);
        }
        [HttpPost("api/app/[controller]/Create")]
        public override Task<TEntityDto> CreateAsync(TCreateInput input)
        {
            ClearCacheAfterUpdateDb();
            return base.CreateAsync(input);
        }

        [HttpPost("api/app/[controller]/Update/{id}")]
        public override Task<TEntityDto> UpdateAsync(TKey id, TCreateInput input)
        {
            ClearCacheAfterUpdateDb();
            ClearCacheById(id);
            return base.UpdateAsync(id, input);
        }
        [HttpPost("api/app/[controller]/RemoveById/{id}")]
        public override Task DeleteAsync(TKey id)
        {
            ClearCacheAfterUpdateDb();
            ClearCacheById(id);
            return base.DeleteAsync(id);
        }

        [HttpPost("api/app/[controller]/GetListToFile")]
        public virtual Task<FileDto> GetListToFileAsync([FromQuery] bool isAll, [FromQuery] int fileType, TGetListInput input)
        {
            return Task.FromResult(
                new FileDto()
            );
        }

        protected virtual IQueryable<TEntityDto> QueryPagedResult(TGetListInput input)
        {
            return null;
        }

        protected virtual void ClearCacheAfterUpdateDb()
        {

        }
        protected virtual void ClearCacheById(TKey id)
        {

        }
    }
}
