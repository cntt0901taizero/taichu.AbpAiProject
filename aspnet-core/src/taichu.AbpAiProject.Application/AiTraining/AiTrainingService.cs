
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using taichu.AbpAiProject.AiTraining.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Collections.Generic;

namespace taichu.AbpAiProject.AiTraining
{
    public interface IAiTrainingService :
        ICrudAppService< 
            AiTrainingDto, 
            long, 
            AiTrainingPagedAndSortedResultRequestDto, 
            AiTrainingDto>
    {

    }

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

    }
}
