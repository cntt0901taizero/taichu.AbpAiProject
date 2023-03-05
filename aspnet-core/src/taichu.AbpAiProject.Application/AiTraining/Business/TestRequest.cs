using AutoMapper;
using BaseApplication.Factory;
using Dapper;
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
using taichu.AbpAiProject.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;

namespace taichu.AbpAiProject.AiTraining.Business
{
    public class TestRequest : IRequest<bool>
    {
        public AiTrainingDto Input { get; set; }
    }
    public class TestHandlerRequest : DapperRepository<AbpAiProjectDbContext>, ITransientDependency, IRequestHandler<TestRequest, bool>
    {
        private readonly IRepository<AiTrainingEntity, long> _repository;
        private readonly IDistributedCache<AiTrainingDto> _cache;
        private readonly IAppFactory _factory;
        public TestHandlerRequest(
            IDbContextProvider<AbpAiProjectDbContext> dbContextProvider, 
            IRepository<AiTrainingEntity, long> repository, 
            IDistributedCache<AiTrainingDto> cache, IAppFactory factory) : base(dbContextProvider)
        {
            _repository = repository;
            _cache = cache;
            _factory = factory;
        }

        [UnitOfWork]
        public async Task<bool> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            var input = request.Input;

            var db = _factory.DefaultDbFactory.Connection;
            var sql = $@"SELECT * FROM Db_Ai_Training";
            var a = await db.QueryFirstAsync<AiTrainingEntity>(sql.ToString());
            var b = (await DbConnection.QueryAsync<string>("SELECT * FROM Db_Ai_Training", transaction: DbTransaction)).ToList();

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
                .Select(x => _factory.ObjectMapper.Map<AiTrainingEntity, AiTrainingDto>(x))
                .ToListAsync();

            try
            {
                var id = 1;
                var keycache = "SeverCacheAiTraing" + id;
                var dataById = await aiTrainingRepos.Where(x => x.Id == id)
                    .Select(x => _factory.ObjectMapper.Map<AiTrainingEntity, AiTrainingDto>(x))
                    .FirstOrDefaultAsync();
                if (dataById == null)
                {
                    await _cache.SetAsync(keycache, dataById, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) });
                }
                _cache.Remove(keycache);
                await _factory.CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _factory.CurrentUnitOfWork.RollbackAsync();
                _factory.CurrentUnitOfWork.Dispose();
            }

            return true;
        }
    }
}
