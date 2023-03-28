using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace ContaCorrente.MongoDB;

[ConnectionStringName("Default")]
public class ContaCorrenteMongoDbContext : AbpMongoDbContext
{
    public IMongoCollection<ContaCorrenteItem> Questions => Collection<ContaCorrenteItem>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.Entity<ContaCorrenteItem>(b =>
        {
           b.CollectionName = "ContaCorrenteItems";
        });
    }
}
