using System;

namespace Domain.Models
{
    public sealed class Turista : Boleto
    {
        private const decimal RecargoTurista = 8400m;

        public Turista(string numero, DateTime fechaSalida, int tiempoEnDias, decimal costoEmbarque)
            : base(numero, fechaSalida, tiempoEnDias, costoEmbarque)
        {
        }

        protected override decimal RecargoTipo() => RecargoTurista;
    }
}
