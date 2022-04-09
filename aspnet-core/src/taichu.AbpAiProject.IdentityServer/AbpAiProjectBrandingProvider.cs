using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace taichu.AbpAiProject;

[Dependency(ReplaceServices = true)]
public class AbpAiProjectBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AbpAiProject";
}
