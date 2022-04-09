using taichu.AbpAiProject.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace taichu.AbpAiProject.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AbpAiProjectController : AbpControllerBase
{
    protected AbpAiProjectController()
    {
        LocalizationResource = typeof(AbpAiProjectResource);
    }
}
