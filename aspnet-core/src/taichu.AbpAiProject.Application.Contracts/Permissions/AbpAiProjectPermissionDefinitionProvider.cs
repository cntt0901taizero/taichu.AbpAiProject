using taichu.AbpAiProject.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace taichu.AbpAiProject.Permissions;

public class AbpAiProjectPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var AbpAiProject = context.AddGroup(AbpAiProjectPermissions.GroupName, L("Permission:AbpAiProject"));

        var AiTraining = AbpAiProject.AddPermission(AbpAiProjectPermissions.AiTraining, L("Permission:AiTraining"));
        var DataTraining = AiTraining.AddChild(AbpAiProjectPermissions.DataTraining.Default, L("Permission:DataTraining"));
        DataTraining.AddChild(AbpAiProjectPermissions.DataTraining.Search, L("Permission:DataTraining.Search"));
        DataTraining.AddChild(AbpAiProjectPermissions.DataTraining.List, L("Permission:DataTraining.List"));
        DataTraining.AddChild(AbpAiProjectPermissions.DataTraining.Create, L("Permission:DataTraining.Create"));
        DataTraining.AddChild(AbpAiProjectPermissions.DataTraining.Update, L("Permission:DataTraining.Update"));
        DataTraining.AddChild(AbpAiProjectPermissions.DataTraining.Delete, L("Permission:DataTraining.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpAiProjectResource>(name);
    }
}
