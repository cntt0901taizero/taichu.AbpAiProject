using taichu.AbpAiProject.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace taichu.AbpAiProject.Permissions;

public class AbpAiProjectPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AbpAiProjectPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(AbpAiProjectPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpAiProjectResource>(name);
    }
}
