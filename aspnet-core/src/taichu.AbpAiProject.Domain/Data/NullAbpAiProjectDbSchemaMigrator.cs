using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace taichu.AbpAiProject.Data;

/* This is used if database provider does't define
 * IAbpAiProjectDbSchemaMigrator implementation.
 */
public class NullAbpAiProjectDbSchemaMigrator : IAbpAiProjectDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
