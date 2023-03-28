using System.Threading.Tasks;

namespace ContaCorrente.Data;

public interface IContaCorrenteDbSchemaMigrator
{
    Task MigrateAsync();
}
