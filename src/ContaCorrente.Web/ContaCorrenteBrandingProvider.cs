using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ContaCorrente.Web;

[Dependency(ReplaceServices = true)]
public class ContaCorrenteBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ContaCorrente";
}
