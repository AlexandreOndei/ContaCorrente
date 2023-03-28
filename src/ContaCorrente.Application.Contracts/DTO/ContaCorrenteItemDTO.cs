using System;

namespace ContaCorrente
{
    public class ContaCorrenteItemDto
    {
        public Guid Id { get; set; }
        public string CPF { get; set; }
        public DateTime Data { get; set; }
        public float ValorTransacao { get; set; }
        public int Tipo { get; set; }
        public DateTime? DeletadoEm { get; set; }
    }
}
