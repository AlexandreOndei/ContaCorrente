using System;
using Volo.Abp.Domain.Entities;

namespace ContaCorrente
{
    public class ContaCorrenteItem : BasicAggregateRoot<Guid>
    {
        public string CPF { get; set; }
        public DateTime Data { get; set; }
        public float ValorTransacao { get; set; }
        public int Tipo { get; set; }
        public DateTime? DeletadoEm { get; set; }
    }
}
