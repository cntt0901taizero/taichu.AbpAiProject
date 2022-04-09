using AutoMapper;
using taichu.AbpAiProject.AiTraining;
using taichu.AbpAiProject.AiTraining.Dto;

namespace taichu.AbpAiProject;

public class AbpAiProjectApplicationAutoMapperProfile : Profile
{
    public AbpAiProjectApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<AiTrainingEntity, AiTrainingDto>().ReverseMap();
    }
}
