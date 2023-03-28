using ContaCorrente.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ContaCorrente.Permissions;

public class ContaCorrentePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ContaCorrentePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ContaCorrentePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ContaCorrenteResource>(name);
    }
}
