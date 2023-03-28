using ContaCorrente.MongoDB;
using Xunit;

namespace ContaCorrente;

[CollectionDefinition(ContaCorrenteTestConsts.CollectionDefinitionName)]
public class ContaCorrenteDomainCollection : ContaCorrenteMongoDbCollectionFixtureBase
{

}
