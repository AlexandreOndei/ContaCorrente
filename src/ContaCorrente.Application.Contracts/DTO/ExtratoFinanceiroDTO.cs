using System;

namespace ContaCorrente
{
    public class ExtratoFinanceiroDTO
    {
        public string CPF { get; set; }
        public DateTime Data { get; set; }
        public float[] Creditos { get; set; }
        public float[] Debitos { get; set; }
        public float Balance { get; set; }
    }
}
