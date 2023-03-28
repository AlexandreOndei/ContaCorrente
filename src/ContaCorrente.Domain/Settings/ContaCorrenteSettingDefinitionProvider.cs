using Volo.Abp.Settings;

namespace ContaCorrente.Settings;

public class ContaCorrenteSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ContaCorrenteSettings.MySetting1));
    }
}
