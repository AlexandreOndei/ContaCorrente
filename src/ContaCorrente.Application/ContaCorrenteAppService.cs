using System;
using System.Collections.Generic;
using System.Text;
using ContaCorrente.Localization;
using Volo.Abp.Application.Services;

namespace ContaCorrente;

/* Inherit your application services from this class.
 */
public abstract class ContaCorrenteAppService : ApplicationService
{
    protected ContaCorrenteAppService()
    {
        LocalizationResource = typeof(ContaCorrenteResource);
    }
}
