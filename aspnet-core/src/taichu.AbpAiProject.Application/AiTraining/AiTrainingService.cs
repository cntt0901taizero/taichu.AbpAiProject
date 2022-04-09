

using System.Threading.Tasks;
using taichu.AbpAiProject.AiTraining.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
        public AiTrainingService(IRepository<AiTrainingEntity, long> repository) : base(repository)
        {
        }
        
    }
}
