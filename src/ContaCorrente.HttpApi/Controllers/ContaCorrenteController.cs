using ContaCorrente.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ContaCorrente.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ContaCorrenteController : AbpControllerBase
{
    protected ContaCorrenteController()
    {
        LocalizationResource = typeof(ContaCorrenteResource);
    }
}
