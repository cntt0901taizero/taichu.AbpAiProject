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
using MediatR;
using taichu.AbpAiProject.AiTraining.Business;

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
        private readonly IMediator _mediator;
        private readonly IRepository<AiTrainingEntity, long> _repository;
        public AiTrainingService(
            IMediator mediator,
            IRepository<AiTrainingEntity, long> repository
            ) : base(repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<bool> Test()
        {
            return await _mediator.Send(new TestRequest());
        }
    }
}
