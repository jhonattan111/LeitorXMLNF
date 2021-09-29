using System;

namespace LeitorXMLNF
{
    class Program
    {
        static void Main(string[] args)
        {
            string Caminho = string.Empty;
            Console.Write("Caminho: ");
            Caminho = Console.ReadLine();

            var Notas = new Leitor(Caminho);

            
            Notas.LerNotas();
            Notas.GravarArquivo();

            Console.WriteLine(Notas.ExibirLogs());

            Console.ReadKey();
        }
    }
}
