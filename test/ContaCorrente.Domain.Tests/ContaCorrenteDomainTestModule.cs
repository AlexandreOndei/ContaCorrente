using ContaCorrente.MongoDB;
using Volo.Abp.Modularity;

namespace ContaCorrente;

[DependsOn(
    typeof(ContaCorrenteMongoDbTestModule)
    )]
public class ContaCorrenteDomainTestModule : AbpModule
{

}
