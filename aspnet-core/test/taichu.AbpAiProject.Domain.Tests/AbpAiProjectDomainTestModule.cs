using taichu.AbpAiProject.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace taichu.AbpAiProject;

[DependsOn(
    typeof(AbpAiProjectEntityFrameworkCoreTestModule)
    )]
public class AbpAiProjectDomainTestModule : AbpModule
{

}
