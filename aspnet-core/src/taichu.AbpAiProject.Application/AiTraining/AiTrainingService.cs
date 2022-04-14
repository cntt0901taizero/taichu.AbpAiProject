using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using taichu.AbpAiProject.AiTraining.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace taichu.AbpAiProject.AiTraining
{
    public interface IAiTrainingService :
        ICrudAppService< 
            AiTrainingDto, 
            long, 
            AiTrainingPagedAndSortedResultRequestDto, 
            AiTrainingDto>
    {}

    public class AiTrainingService :
        CrudAppService<
            AiTrainingEntity,
            AiTrainingDto,
            long,
            AiTrainingPagedAndSortedResultRequestDto,
            AiTrainingDto>,
        IAiTrainingService  
    {
        private IRepository<AiTrainingEntity, long> _repository;
        public AiTrainingService(IRepository<AiTrainingEntity, long> repository) : base(repository)
        {
            _repository = repository;
        }

        //public async Task<List<AiTrainingDto>> Test(AiTrainingPagedAndSortedResultRequestDto input)
        //{
        //    IQueryable<AiTrainingEntity> queryable = await _repository.GetQueryableAsync();
        //    var data = await queryable.Where(x => x.InputString.Contains(input.Filter))
        //        .Select(x => ObjectMapper.Map<AiTrainingEntity, AiTrainingDto>(x))
        //        .ToListAsync();
        //    return data;
        //}
    }
}
