using System.Threading.Tasks;

namespace taichu.AbpAiProject.Data;

public interface IAbpAiProjectDbSchemaMigrator
{
    Task MigrateAsync();
}
