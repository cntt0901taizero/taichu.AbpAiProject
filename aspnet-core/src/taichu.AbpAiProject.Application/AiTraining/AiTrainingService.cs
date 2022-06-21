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
using taichu.AbpAiProject.Permissions;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.DependencyInjection;
using taichu.AbpAiProject.EntityFrameworkCore;
using BaseApplication.Factory;
using Microsoft.AspNetCore.Authorization;

namespace taichu.AbpAiProject.AiTraining
{
    //[Authorize]
    public class AiTrainingService :
        AbpAiProjectCrudAppService<
            AiTrainingEntity,
            AiTrainingDto,
            long,
            AiTrainingPagedAndSortedResultRequestDto,
            AiTrainingDto>
    {
        private readonly IMediator _mediator;
        public AiTrainingService(
            IAppFactory appFactory,
            IMediator mediator
            ) : base(appFactory)
        {
            _mediator = mediator;

            GetPolicyName = AbpAiProjectPermissions.DataTraining.Default;
            GetListPolicyName = AbpAiProjectPermissions.DataTraining.Default;
            CreatePolicyName = AbpAiProjectPermissions.DataTraining.Create;
            UpdatePolicyName = AbpAiProjectPermissions.DataTraining.Update;
            DeletePolicyName = AbpAiProjectPermissions.DataTraining.Delete;
        }

        public async Task<long> Test()
        {
            var res = await _mediator.Send(new TestRequest());
            return 1;
        }

    }
}
