using Domain.Contracts;
using System;

namespace Domain.Models
{
    public abstract class Boleto : IEntity
    {
        private const decimal CostoBase = 9950m;

        protected Boleto(string numero, DateTime fechaSalida, int tiempoEnDias, decimal costoEmbarque)
        {
            Id = Guid.NewGuid();
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
            FechaSalida = fechaSalida;
            TiempoEnDias = tiempoEnDias;
            CostoEmbarque = costoEmbarque;
        }

        public Guid Id { get; set; }

        public string Numero { get; }

        public DateTime FechaSalida { get; }

        public int TiempoEnDias { get; }

        public decimal CostoEmbarque { get; }

        public DateTime CalcularRegreso()
        {
            return FechaSalida.AddDays(TiempoEnDias);
        }

        public decimal CostoBoleto()
        {
            return CostoBase + RecargoTipo() + CostoEmbarque;
        }

        protected abstract decimal RecargoTipo();
    }
}
