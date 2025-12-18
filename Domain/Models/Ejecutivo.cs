using System;

namespace Domain.Models
{
    public sealed class Ejecutivo : Boleto
    {
        private const decimal RecargoEjecutivo = 9800m;

        public Ejecutivo(string numero, DateTime fechaSalida, int tiempoEnDias, decimal costoEmbarque)
            : base(numero, fechaSalida, tiempoEnDias, costoEmbarque)
        {
        }

        protected override decimal RecargoTipo() => RecargoEjecutivo;
    }
}
