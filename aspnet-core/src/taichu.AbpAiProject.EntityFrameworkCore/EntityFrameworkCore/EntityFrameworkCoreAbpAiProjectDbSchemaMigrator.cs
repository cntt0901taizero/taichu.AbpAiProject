using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using taichu.AbpAiProject.Data;
using Volo.Abp.DependencyInjection;

namespace taichu.AbpAiProject.EntityFrameworkCore;

public class EntityFrameworkCoreAbpAiProjectDbSchemaMigrator
    : IAbpAiProjectDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAbpAiProjectDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the AbpAiProjectDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AbpAiProjectDbContext>()
            .Database
            .MigrateAsync();
    }
}
