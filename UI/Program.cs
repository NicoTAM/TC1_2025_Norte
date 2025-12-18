using BLL.Implementations;
using Domain.Models;
using System;

namespace UI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var boletoService = new BoletoService();

            Turista boletoTurista = new Turista(
                numero: "TUR-001",
                fechaSalida: new DateTime(2025, 5, 10),
                tiempoEnDias: 7,
                costoEmbarque: 2500m);

            Ejecutivo boletoEjecutivo = new Ejecutivo(
                numero: "EJE-001",
                fechaSalida: new DateTime(2025, 6, 15),
                tiempoEnDias: 4,
                costoEmbarque: 3200m);

            boletoService.Registrar(boletoTurista);
            boletoService.Registrar(boletoEjecutivo);

            Console.WriteLine("Detalle de boletos registrados:\n");

            foreach (var boleto in boletoService.ObtenerTodos())
            {
                Console.WriteLine($"Boleto: {boleto.Numero}");
                Console.WriteLine($" - Fecha de salida: {boleto.FechaSalida:dd/MM/yyyy}");
                Console.WriteLine($" - Fecha de regreso: {boleto.CalcularRegreso():dd/MM/yyyy}");
                Console.WriteLine($" - Costo final: ${boleto.CostoBoleto():N2}\n");
            }

            Console.WriteLine("Presione una tecla para salir...");
            Console.ReadKey();
        }
    }
}
