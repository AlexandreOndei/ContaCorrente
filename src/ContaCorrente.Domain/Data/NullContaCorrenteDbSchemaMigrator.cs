using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ContaCorrente.Data;

/* This is used if database provider does't define
 * IContaCorrenteDbSchemaMigrator implementation.
 */
public class NullContaCorrenteDbSchemaMigrator : IContaCorrenteDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
