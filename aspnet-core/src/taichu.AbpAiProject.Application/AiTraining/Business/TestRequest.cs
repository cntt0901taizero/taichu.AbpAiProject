using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using taichu.AbpAiProject.AiTraining.Dto;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace taichu.AbpAiProject.AiTraining.Business
{
    public class TestRequest : IRequest<bool>
    {

    }
    public class TestHandlerRequest : ApplicationService, IRequestHandler<TestRequest, bool>
    {
        private readonly IRepository<AiTrainingEntity, long> _repository;
        private readonly IDistributedCache<AiTrainingDto> _cache;
        public TestHandlerRequest(IRepository<AiTrainingEntity, long> repository, IDistributedCache<AiTrainingDto> cache)
        {
            _repository = repository;
            _cache = cache;
        }

        [UnitOfWork]
        public async Task<bool> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            IQueryable<AiTrainingEntity> aiTrainingRepos = await _repository.GetQueryableAsync();

            var query = from item in aiTrainingRepos
                        where item.FuncName == "test"
                        select new AiTrainingDto { 
                            Id = item.Id,
                            FuncName = item.FuncName,
                            InputString = item.InputString,
                            OutputString = item.OutputString,
                            Link = item.Link,
                            Note = item.Note,
                        };
            var data1 = await query.ToListAsync();

            var data2 = await aiTrainingRepos
                .Where(x => x.FuncName == "test")
                .Select(x => ObjectMapper.Map<AiTrainingEntity, AiTrainingDto>(x))
                .ToListAsync();

            try
            {
                var id = 1;
                var keycache = "SeverCacheAiTraing" + id;
                var dataById = await aiTrainingRepos.Where(x => x.Id == id)
                    .Select(x => ObjectMapper.Map<AiTrainingEntity, AiTrainingDto>(x))
                    .FirstOrDefaultAsync();
                if (dataById == null)
                {
                    await _cache.SetAsync(keycache, dataById, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) });
                }
                _cache.Remove(keycache);
                await UnitOfWorkManager.Current.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await UnitOfWorkManager.Current.RollbackAsync();
                UnitOfWorkManager.Current.Dispose();
            }
            

            return true;
        }
    }
}
