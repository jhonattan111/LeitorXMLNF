using System;
using System.Data;
using System.Data.SQLite;
using LeitorXMLNF.Models.NFe;

namespace LeitorXMLNF.Models.SQLite
{
    public class SQHelper
    {
        private static SQLiteConnection _sqliteConnection;
        private static string _caminho;
        public SQHelper(string caminho)
        { 
            _caminho = caminho;
        }

        private SQLiteConnection DbConnection()
        {
            _sqliteConnection = new SQLiteConnection($"Data Source={_caminho}\\Cadastro.sqlite; Version=3;");
            _sqliteConnection.Open();
            return _sqliteConnection;
        }
        public void CriarBancoSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile("Cadastro.sqlite");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS NFe(id INTEGER PRIMARY KEY AUTOINCREMENT, dhEmi DATETIME, dhSaiEnt DATETIME, nNF VARCHAR(44), xNome VARCHAR(200))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetNFe()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM NFe";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetNFe(int id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM NFe Where Id=" + id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Add(NFeProc nfe)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO NFe(dhEmi, dhSaiEnt, nNF, xNome) values (@dhEmi, @dhSaiEnt, @nNF, @xNome)";
                    cmd.Parameters.AddWithValue("@dhEmi", nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.dhEmi.ToString("dd/MM/yyyy hh:mm:ss"));
                    cmd.Parameters.AddWithValue("@dhSaiEnt", (nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.dhSaiEnt.HasValue ? nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.dhSaiEnt.Value.ToString("dd/MM/yyyy hh:mm:ss") : ""));
                    cmd.Parameters.AddWithValue("@nNF", nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.nNF);
                    cmd.Parameters.AddWithValue("@xNome", nfe.NotaFiscalEletronica.InformacoesNFe.Emitente.xNome);
                    cmd.Parameters.AddWithValue("@chNFe", nfe.NotaFiscalEletronica.InformacoesNFe.InformacaoAdicional.chNFe)
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
