using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace ContaCorrente.MongoDB;

[DependsOn(
    typeof(ContaCorrenteTestBaseModule),
    typeof(ContaCorrenteMongoDbModule)
    )]
public class ContaCorrenteMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = ContaCorrenteMongoDbFixture.ConnectionString.Split('?');
        var connectionString = stringArray[0].EnsureEndsWith('/') +
                                   "Db_" +
                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });
    }
}
