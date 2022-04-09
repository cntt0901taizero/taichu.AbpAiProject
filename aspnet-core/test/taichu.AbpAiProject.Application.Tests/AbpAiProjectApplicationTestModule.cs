using Volo.Abp.Modularity;

namespace taichu.AbpAiProject;

[DependsOn(
    typeof(AbpAiProjectApplicationModule),
    typeof(AbpAiProjectDomainTestModule)
    )]
public class AbpAiProjectApplicationTestModule : AbpModule
{

}
