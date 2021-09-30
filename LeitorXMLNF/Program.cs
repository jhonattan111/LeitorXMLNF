using System;

namespace LeitorXMLNF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("+---------------- LEITOR DE NOTAS FISCAIS ----------------+");
            Console.ForegroundColor = ConsoleColor.White;
            string Caminho = string.Empty;


            while (Console.ReadKey().Key != ConsoleKey.S)
            {
                Console.Write("MENU?: Y - INICIAR LEITURA | S = SAIR ");
                Console.WriteLine("");
                Console.Write("| Caminho: ");
                Caminho = Console.ReadLine();

                var Notas = new Leitor(Caminho);

            
                Notas.LerNotas();
                Notas.GravarArquivo();

                Console.WriteLine(Notas.ExibirLogs());

                Console.WriteLine("+-------------------------------------------------------+");
            }
        }
    }
}
