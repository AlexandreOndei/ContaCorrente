using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ContaCorrente
{
    public interface IContaCorrenteService : IApplicationService
    {
        Task<List<ExtratoFinanceiroDTO>> GetListAsync(string cpf, DateTime dataInicial, DateTime dataFinal, int? tipo);
        Task<ContaCorrenteItemDto> CreateAsync(ContaCorrenteItemDto contaCorrenteItemDto);
        Task DeleteAsync(Guid id);
    }
}
