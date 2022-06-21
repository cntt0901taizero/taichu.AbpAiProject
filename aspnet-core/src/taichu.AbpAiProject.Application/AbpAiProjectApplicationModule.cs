using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.Dapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace taichu.AbpAiProject;

[DependsOn(
    typeof(AbpAiProjectDomainModule),
    typeof(AbpDapperModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpAiProjectApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class AbpAiProjectApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpAiProjectApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AbpAiProjectApplicationModule>();
        });
        // Cấu hình MediatR
        context.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
        context.Services.AddMediatR(typeof(AbpAiProjectApplicationModule).GetTypeInfo().Assembly);
    }
}
