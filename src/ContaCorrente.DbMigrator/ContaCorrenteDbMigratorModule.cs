using ContaCorrente.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ContaCorrente.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ContaCorrenteMongoDbModule),
    typeof(ContaCorrenteApplicationContractsModule)
    )]
public class ContaCorrenteDbMigratorModule : AbpModule
{

}
