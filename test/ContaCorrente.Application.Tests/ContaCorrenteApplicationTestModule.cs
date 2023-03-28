using Volo.Abp.Modularity;

namespace ContaCorrente;

[DependsOn(
    typeof(ContaCorrenteApplicationModule),
    typeof(ContaCorrenteDomainTestModule)
    )]
public class ContaCorrenteApplicationTestModule : AbpModule
{

}
