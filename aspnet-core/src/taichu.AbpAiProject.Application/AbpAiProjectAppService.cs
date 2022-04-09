using System;
using System.Collections.Generic;
using System.Text;
using taichu.AbpAiProject.Localization;
using Volo.Abp.Application.Services;

namespace taichu.AbpAiProject;

/* Inherit your application services from this class.
 */
public abstract class AbpAiProjectAppService : ApplicationService
{
    protected AbpAiProjectAppService()
    {
        LocalizationResource = typeof(AbpAiProjectResource);
    }
}
