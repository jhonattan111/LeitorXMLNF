using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeitorXMLNF.Models.NFe
{
    public class Identificacao
    {
        public int cUF { get; set; }
        public string cNF { get; set; }
        public string natOp { get; set; }
        public string mod { get; set; }
        public int serie { get; set; }
        public string nNF { get; set; }
        public DateTime dhEmi { get; set; }
        public DateTime dhSaiEnt { get; set; }
        public int tpNF { get; set; }
        public int idDest { get; set; }
        public int cMunFG {  get; set; }
        public int tpImp { get; set; }
        public int tpEmis { get; set; }
        public int cDV { get; set; }
        public int tpAmb { get; set; }
        public int finNFe {  get; set; }
        public int indFinal { get; set; }
        public int indPres { get; set; }
        public int procEmi { get; set; }
        public string verProc { get; set; }
        public int indPag { get; set; }
    }
}
