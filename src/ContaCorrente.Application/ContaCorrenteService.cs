using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ContaCorrente
{
    public class ContaCorrenteService : ApplicationService, IContaCorrenteService
    {
        private readonly IRepository<ContaCorrenteItem, Guid> _contaCorrenteItemRepository;

        public ContaCorrenteService(IRepository<ContaCorrenteItem, Guid> contaCorrenteItemRepository)
        {
            _contaCorrenteItemRepository = contaCorrenteItemRepository;
        }

        public async Task<ContaCorrenteItemDto> CreateAsync(ContaCorrenteItemDto contaCorrenteItemDto)
        {
            var contaCorrenteItem = await _contaCorrenteItemRepository.InsertAsync(
                new ContaCorrenteItem
                {
                    CPF = contaCorrenteItemDto.CPF,
                    Data = DateTime.Now,
                    Tipo = contaCorrenteItemDto.Tipo,
                    ValorTransacao = contaCorrenteItemDto.ValorTransacao
                }
            );

            return new ContaCorrenteItemDto
            {
                Id = contaCorrenteItem.Id,
                CPF = contaCorrenteItem.CPF,
                Data = contaCorrenteItem.Data,
                DeletadoEm = contaCorrenteItem.DeletadoEm,
                Tipo = contaCorrenteItem.Tipo,
                ValorTransacao = contaCorrenteItem.ValorTransacao
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            ContaCorrenteItem contaCorrenteItem =
                await _contaCorrenteItemRepository.GetAsync(item => item.Id == id);
            contaCorrenteItem.DeletadoEm = DateTime.Now;
            await _contaCorrenteItemRepository.UpdateAsync(contaCorrenteItem);
        }

        public async Task<List<ExtratoFinanceiroDTO>> GetListAsync(
            string cpf, DateTime dataInicial, DateTime dataFinal, int? tipo)
        {
            var items = await _contaCorrenteItemRepository.GetListAsync();
            return items
                .Where(item => item.CPF == cpf &&
                               item.Data.Date >= dataInicial.Date && item.Data.Date <= dataFinal.Date &&
                               !item.DeletadoEm.HasValue &&
                               (tipo.HasValue ? item.Tipo == tipo : true))
                .GroupBy(item => new { item.CPF, Data = item.Data.Date })
                .Select(item =>
                {
                    IEnumerable<ContaCorrenteItem> debitosDia = 
                        !tipo.HasValue || tipo.GetValueOrDefault() == (int)TipoContaCorrenteEnum.Debito ?
                        item.Where(g => g.Tipo == (int)TipoContaCorrenteEnum.Debito) : null;
                    IEnumerable<ContaCorrenteItem> creditosDia =
                        !tipo.HasValue || tipo.GetValueOrDefault() == (int)TipoContaCorrenteEnum.Credito ?
                        item.Where(g => g.Tipo == (int)TipoContaCorrenteEnum.Credito) : null;
                    IEnumerable<ContaCorrenteItem> debitosGeral = 
                        !tipo.HasValue || tipo.GetValueOrDefault() == (int)TipoContaCorrenteEnum.Debito ?
                        items.Where(i => i.Tipo == (int)TipoContaCorrenteEnum.Debito && i.Data.Date <= item.Key.Data.Date && !i.DeletadoEm.HasValue) : null;
                    IEnumerable<ContaCorrenteItem> creditosGeral =
                        !tipo.HasValue || tipo.GetValueOrDefault() == (int)TipoContaCorrenteEnum.Credito ?
                        items.Where(i => i.Tipo == (int)TipoContaCorrenteEnum.Credito && i.Data.Date <= item.Key.Data.Date && !i.DeletadoEm.HasValue) : null;
                    return new ExtratoFinanceiroDTO
                    {
                        CPF = item.Key.CPF,
                        Data = item.Key.Data,
                        Creditos = creditosDia?.Select(g => g.ValorTransacao)?.ToArray(),
                        Debitos = debitosDia?.Select(g => g.ValorTransacao)?.ToArray(),
                        Balance = Math.Abs((creditosGeral != null ? creditosGeral.Sum(g => g.ValorTransacao) : 0) - (debitosGeral != null ? debitosGeral.Sum(g => g.ValorTransacao) : 0))
                    };
                })
                .OrderBy(o => o.CPF)
                .ThenBy(o => o.Data)
                .ToList();
        }
    }
}
