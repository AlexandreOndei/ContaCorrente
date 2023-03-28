using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Xunit;

namespace ContaCorrente;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
[Collection(ContaCorrenteTestConsts.CollectionDefinitionName)]
public class ContaCorrenteServiceTests : ContaCorrenteApplicationTestBase
{
    private readonly IContaCorrenteService _contaCorrenteService;
    private readonly IRepository<ContaCorrenteItem, Guid> _contaCorrenteRepository = Substitute.For<IRepository<ContaCorrenteItem, Guid>>();

    public ContaCorrenteServiceTests()
    {
        _contaCorrenteService = new ContaCorrenteService(_contaCorrenteRepository);
    }

    private async Task<List<ContaCorrenteItem>> GetRepositoryMockList()
    {
        return await Task.Run(() =>
        {
            return new List<ContaCorrenteItem>
            {
                new ContaCorrenteItem
                {
                    CPF = "92136533010",
                    Data = Convert.ToDateTime("2023-03-20"),
                    Tipo = (int)TipoContaCorrenteEnum.Credito,
                    ValorTransacao = 5000
                },
                new ContaCorrenteItem
                {
                    CPF = "92136533010",
                    Data = Convert.ToDateTime("2023-03-20"),
                    Tipo = (int)TipoContaCorrenteEnum.Debito,
                    ValorTransacao = 1000
                },
                new ContaCorrenteItem
                {
                    CPF = "92136533010",
                    Data = Convert.ToDateTime("2023-03-20"),
                    Tipo = (int)TipoContaCorrenteEnum.Debito,
                    ValorTransacao = 250
                },
                new ContaCorrenteItem
                {
                    CPF = "92136533010",
                    Data = Convert.ToDateTime("2023-03-21"),
                    Tipo = (int)TipoContaCorrenteEnum.Credito,
                    ValorTransacao = 2000
                },
                new ContaCorrenteItem
                {
                    CPF = "92136533010",
                    Data = Convert.ToDateTime("2023-03-21"),
                    Tipo = (int)TipoContaCorrenteEnum.Debito,
                    ValorTransacao = 300
                }
            };
        });
    }

    [Fact]
    public async Task GetListAsync_WithoutTipo_Should_Return_FullExtrato()
    {
        //Arrange
        Task<List<ContaCorrenteItem>> repositoryList = GetRepositoryMockList();
        List<ExtratoFinanceiroDTO> resultList = new List<ExtratoFinanceiroDTO>
        {
            new ExtratoFinanceiroDTO
            {
                Data = Convert.ToDateTime("2023-03-20"),
                CPF = "92136533010",
                Creditos = new float[]{ 5000 },
                Debitos = new float[]{ 1000, 250 },
                Balance = 3750
            },
            new ExtratoFinanceiroDTO
            {
                Data = Convert.ToDateTime("2023-03-21"),
                CPF = "92136533010",
                Creditos = new float[]{ 2000 },
                Debitos = new float[]{ 300 },
                Balance = 5450
            }
        };

        //Mock
        _contaCorrenteRepository.GetListAsync().Returns(repositoryList);

        //Act
        var result = await _contaCorrenteService
            .GetListAsync(
                "92136533010",
                Convert.ToDateTime("2023-03-20"),
                Convert.ToDateTime("2023-03-21"),
                null);

        //Assert
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(result[i].CPF, resultList[i].CPF);
            Assert.Equal(result[i].Data, resultList[i].Data);
            Assert.Equal(result[i].Creditos, resultList[i].Creditos);
            Assert.Equal(result[i].Debitos, resultList[i].Debitos);
            Assert.Equal(result[i].Balance, resultList[i].Balance);
        }
    }

    [Fact]
    public async Task GetListAsync_WithTipoCredito_Should_Return_CreditoExtrato()
    {
        //Arrange
        Task<List<ContaCorrenteItem>> repositoryList = GetRepositoryMockList();
        List<ExtratoFinanceiroDTO> resultList = new List<ExtratoFinanceiroDTO>
        {
            new ExtratoFinanceiroDTO
            {
                Data = Convert.ToDateTime("2023-03-20"),
                CPF = "92136533010",
                Creditos = new float[]{ 5000 },
                Balance = 5000
            },
            new ExtratoFinanceiroDTO
            {
                Data = Convert.ToDateTime("2023-03-21"),
                CPF = "92136533010",
                Creditos = new float[]{ 2000 },
                Balance = 7000
            }
        };

        //Mock
        _contaCorrenteRepository.GetListAsync().Returns(repositoryList);

        //Act
        var result = await _contaCorrenteService
            .GetListAsync(
                "92136533010",
                Convert.ToDateTime("2023-03-20"),
                Convert.ToDateTime("2023-03-21"),
                (int)TipoContaCorrenteEnum.Credito);

        //Assert
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(result[i].CPF, resultList[i].CPF);
            Assert.Equal(result[i].Data, resultList[i].Data);
            Assert.Equal(result[i].Creditos, resultList[i].Creditos);
            Assert.Equal(result[i].Debitos, resultList[i].Debitos);
            Assert.Equal(result[i].Balance, resultList[i].Balance);
        }
    }

    [Fact]
    public async Task GetListAsync_WithTipoDebito_Should_Return_DebitoExtrato()
    {
        //Arrange
        Task<List<ContaCorrenteItem>> repositoryList = GetRepositoryMockList();
        List<ExtratoFinanceiroDTO> resultList = new List<ExtratoFinanceiroDTO>
        {
            new ExtratoFinanceiroDTO
            {
                Data = Convert.ToDateTime("2023-03-20"),
                CPF = "92136533010",
                Debitos = new float[]{ 1000, 250 },
                Balance = 1250
            },
            new ExtratoFinanceiroDTO
            {
                Data = Convert.ToDateTime("2023-03-21"),
                CPF = "92136533010",
                Debitos = new float[]{ 300 },
                Balance = 1550
            }
        };

        //Mock
        _contaCorrenteRepository.GetListAsync().Returns(repositoryList);

        //Act
        var result = await _contaCorrenteService
            .GetListAsync(
                "92136533010",
                Convert.ToDateTime("2023-03-20"),
                Convert.ToDateTime("2023-03-21"),
                (int)TipoContaCorrenteEnum.Debito);

        //Assert
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(result[i].CPF, resultList[i].CPF);
            Assert.Equal(result[i].Data, resultList[i].Data);
            Assert.Equal(result[i].Creditos, resultList[i].Creditos);
            Assert.Equal(result[i].Debitos, resultList[i].Debitos);
            Assert.Equal(result[i].Balance, resultList[i].Balance);
        }
    }
}
