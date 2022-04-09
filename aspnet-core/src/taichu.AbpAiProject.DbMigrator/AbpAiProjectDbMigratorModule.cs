using taichu.AbpAiProject.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace taichu.AbpAiProject.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpAiProjectEntityFrameworkCoreModule),
    typeof(AbpAiProjectApplicationContractsModule)
    )]
public class AbpAiProjectDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
