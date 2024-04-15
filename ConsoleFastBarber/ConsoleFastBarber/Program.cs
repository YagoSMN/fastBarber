using System;
using System.Threading.Tasks;

namespace ConsoleFastBarber
{
    public class Program
    {
        private static Contexto _contexto;

        static void Main(string[] args)
        {
            // Inicializar o contexto (se necessário)
            _contexto = new Contexto();

            // Iniciar a execução em segundo plano
            Task.Run(async () =>
            {
                while (true)
                {
                    // Executar a procedure para cancelar os cortes
                    _contexto.ExecutaProcedure("FBSPJOB_CancelarCortes");
                    Console.WriteLine("Executado!");
                    // Aguardar 10 minutos antes de executar novamente
                    await Task.Delay(TimeSpan.FromMinutes(10));
                }
            });

            // Aguardar indefinidamente
            Task.Delay(-1).Wait();
        }
    }
}
