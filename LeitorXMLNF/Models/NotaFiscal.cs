using System;

namespace LeitorXMLNF.Models
{
    public class NotaFiscal
    {
        public NotaFiscal(string numeroNF, string nomeFornecedor, string informacaoAdicionalFisco, string informacaoAdicional, DateTime dataEntradaSaida, DateTime dataEmissao)
        {
            NumeroNF = numeroNF;
            NomeFornecedor = nomeFornecedor;
            InformacaoAdicional = informacaoAdicional;
            InformacaoAdicionalFisco = informacaoAdicionalFisco;
            DataEntradaSaida = dataEntradaSaida;
            DataEmissao = dataEmissao;
        }

        public string NumeroNF { get; set; }
        public string NomeFornecedor { get; set; }
        public string InformacaoAdicionalFisco { get; set; }
        public string InformacaoAdicional { get; set; }
        public DateTime DataEntradaSaida { get; set; }
        public DateTime DataEmissao { get; set; }
    }
}
