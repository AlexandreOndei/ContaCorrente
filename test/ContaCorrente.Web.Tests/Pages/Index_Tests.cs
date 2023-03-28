using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ContaCorrente.Pages;

[Collection(ContaCorrenteTestConsts.CollectionDefinitionName)]
public class Index_Tests : ContaCorrenteWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
