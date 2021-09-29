using LeitorXMLNF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeitorXMLNF
{
    public class Leitor
    {
        public Leitor(string caminho)
        {
            _caminho = caminho;
            NotasFiscais = new List<NotaFiscal>();
            _log = new List<string>();
        }
        private string _caminho { get; set; }
        private List<string> _diretorios { get; set; }
        private List<string> _log { get; set; }
        public List<NotaFiscal> NotasFiscais { get; set; }

        public void LerNotas()
        {
            if (!Directory.Exists(_caminho))
                _log.Add("O caminho não existe");

            ListarDiretorios();

            var files = Directory.GetFiles(_caminho, @"*.xml").ToList();

            foreach(var diretorio in _diretorios)
                files.AddRange(Directory.GetFiles($"{diretorio}", @"*.xml").ToList());

            foreach (var file in files)
                using (var sr = new StreamReader(file))
                    Serializar(sr.ReadToEnd());

            _log.Add("O arquivo foi gravado com sucesso");
        }

        private void ListarDiretorios()
        {
            _diretorios = Directory.GetDirectories(_caminho).ToList();

            foreach (var item in _diretorios)
            {
                var diretorios = Directory.GetDirectories(item).ToList();
                _diretorios.AddRange(diretorios);

                if (diretorios.Count > 0)
                    ListarDiretorios();
            }
        }

        public void Serializar(string arquivo)
        {
            string nNF = LerTag("ide", "nNF", arquivo);
            string xNome = LerTag("emit", "xNome", arquivo);
            string infAdFisco = LerTag("infAdic", "infAdFisco", arquivo);
            string infAdCpl = LerTag("infAdic", "infAdCpl", arquivo);

            string dataEntradaSaida = LerTag("ide", "dhSaiEnt", arquivo);
            DateTime dhSaiEnt = !string.IsNullOrWhiteSpace(dataEntradaSaida) ? DateTime.Parse(dataEntradaSaida) : new DateTime(1900,01,01);

            string dataEmissao = LerTag("ide", "dhEmi", arquivo);
            DateTime dhEmi = !string.IsNullOrWhiteSpace(dataEmissao) ? DateTime.Parse(dataEntradaSaida) : new DateTime(1900, 01, 01);

            var notaFiscal = new NotaFiscal(nNF, xNome, infAdFisco, infAdCpl, dhSaiEnt, dhEmi);

            NotasFiscais.Add(notaFiscal);
        }

        private static string LerTag(string paiTag, string pTag, string pConteudo)
        {
            string TIP = $"<{paiTag}>";
            string TFP = $"</{paiTag}>";
            string RetornoP = "";

            int P1P = pConteudo.IndexOf(TIP);
            if (P1P > 0)
            {
                int P2P = pConteudo.IndexOf(TFP);
                RetornoP = pConteudo.Substring(P1P + TIP.Length, P2P - P1P - TIP.Length);
            }

            string TI = $"<{pTag}>";
            string TF = $"</{pTag}>";
            string Retorno = "";

            int P1 = RetornoP.IndexOf(TI);
            if (P1 >= 0)
            {
                int P2 = RetornoP.IndexOf(TF);
                Retorno = RetornoP.Substring(P1 + TI.Length, P2 - P1 - TI.Length);
            }

            return Retorno;
        }

        public void GravarArquivo()
        {
            using (var sw = new StreamWriter($"notas{DateTime.Now:ddMMyyyyHHmmss}.txt"))
            {
                sw.WriteLine("Data Emissão\tData Entrada/Saida\tNúmeroNF\tFornecedor\tInformação Adicional\tInformação Adicional ao Fisco");
                foreach (var item in NotasFiscais)
                {
                    var Linha = $"{item.DataEmissao:dd/MM/yyyy HH:mm:ss}\t{item.DataEntradaSaida:dd/MM/yyyy HH:mm:ss}\t{item.NumeroNF}\t{item.NomeFornecedor}\t{item.InformacaoAdicional}\t{item.InformacaoAdicionalFisco}";
                    sw.WriteLine(Linha);
                }

                sw.Close();
            }

        }

        public string ExibirLogs()
        {
            return string.Join("\n",_log);
        }
    }
}
