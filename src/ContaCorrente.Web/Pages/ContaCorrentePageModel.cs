using ContaCorrente.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ContaCorrente.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ContaCorrentePageModel : AbpPageModel
{
    protected ContaCorrentePageModel()
    {
        LocalizationResourceType = typeof(ContaCorrenteResource);
    }
}
