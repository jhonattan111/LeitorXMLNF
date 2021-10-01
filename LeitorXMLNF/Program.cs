using System;

namespace LeitorXMLNF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("+---------------- LEITOR DE NOTAS FISCAIS DA CONTAR ----------------+");
            Console.ForegroundColor = ConsoleColor.White;
            string Caminho = string.Empty;


            Console.WriteLine("");
            Console.Write("Caminho: ");
            Caminho = Console.ReadLine();

            var Notas = new Leitor(Caminho);

            
            Notas.LerNotas();
            Notas.GravarArquivo();

            Console.WriteLine(Notas.ExibirLogs());
        }
    }
}
