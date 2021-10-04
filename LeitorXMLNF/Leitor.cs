using LeitorXMLNF.Models;
using LeitorXMLNF.Models.NFe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LeitorXMLNF
{
    public class Leitor
    {
        public Leitor(string caminho)
        {
            _caminho = caminho;
            NotasFiscais = new List<NFeProc>();
            _diretorios = new List<string>(){ _caminho };
            _log = new List<string>();
        }
        private string _caminho { get; set; }
        private List<string> _diretorios { get; set; }
        private List<string> _log { get; set; }
        public List<NFeProc> NotasFiscais { get; set; }

        public void LerNotas()
        {
            if (!Directory.Exists(_caminho))
            {
                _log.Add("O caminho não existe");
                return;
            }

            CarregarDiretorios();

            var padraoXML = new Regex(@"[0-9]{44}\.xml");

            var arquivos = new List<string>();

            foreach(var diretorio in _diretorios)
                arquivos.AddRange(Directory.GetFiles(diretorio, @"*.xml").Where(x => padraoXML.IsMatch(x)).ToList());

            var leitor = new LeitorXML();

            foreach (var file in arquivos)
            {
                var nota = leitor.DesserializarXML<NFeProc>(file);
                NotasFiscais.Add(nota);
            }

            _log.Add("O arquivo foi gravado com sucesso");
        }

        private void CarregarDiretorios()
        {
            var diretoriosRaiz = Directory.GetDirectories(_caminho).ToList();

            _diretorios.AddRange(diretoriosRaiz);

            foreach (var item in diretoriosRaiz)
                CarregarSubdiretorios(item);
        }

        private void CarregarSubdiretorios(string diretorio)
        {
            var subdiretorios = Directory.GetDirectories(diretorio).ToList();
            _diretorios.AddRange(subdiretorios);
            
            foreach (var item in subdiretorios)
                CarregarSubdiretorios(item);
        }

        private static string LerTag(string tagPai, string tagFilho, string pConteudo)
        {
            string TIP = $"<{tagPai}>";
            string TFP = $"</{tagPai}>";
            string RetornoP = "";

            int P1P = pConteudo.IndexOf(TIP);
            if (P1P > 0)
            {
                int P2P = pConteudo.IndexOf(TFP);
                RetornoP = pConteudo.Substring(P1P + TIP.Length, P2P - P1P - TIP.Length);
            }

            string TI = $"<{tagFilho}>";
            string TF = $"</{tagFilho}>";
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
            string _novoArquivo = $"notas{DateTime.Now:ddMMyyyyHHmmss}.txt";

            if (File.Exists(_novoArquivo))
            {
                _log.Add($"O arquivo {_novoArquivo} já existe");
                return;
            }

            using (var sw = new StreamWriter(_novoArquivo))
            {
                sw.WriteLine("Data Emissão\tData Entrada/Saida\tNúmeroNF\tFornecedor\tInformação Adicional\tInformação Adicional ao Fisco");
                
                foreach(var nota in NotasFiscais)
                {
                    sw.WriteLine($"{nota.NotaFiscalEletronica.InformacoesNFe.Identificacao.dhEmi.ToString("dd/MM/yyyy")}\t" +
                                 $"{nota.NotaFiscalEletronica.InformacoesNFe.Identificacao.dhSaiEnt.ToString("dd/MM/yyyy")}\t" +
                                 $"{nota.NotaFiscalEletronica.InformacoesNFe.Identificacao.nNF}\t" +
                                 $"{nota.NotaFiscalEletronica.InformacoesNFe.Emitente.xNome}\t" +
                                 $"{nota.NotaFiscalEletronica.InformacoesNFe.InformacaoAdicional.infCpl}\t" +
                                 $"{nota.NotaFiscalEletronica.InformacoesNFe.InformacaoAdicional.infAdFisco}\t");
                }

                sw.Close();
            }
        }

        public string ExibirLogs()
        {
            return string.Join("\n", _log);
        }
    }
}
