using System;
using System.Xml.Serialization;

namespace LeitorXMLNF
{
    public class LeitorXML
    {
        public T DesserializarXML<T>(string arquivo) where T : class
        {
            var serializar = new XmlSerializer(typeof(T));

            try
            {
                var xmlArquivo = System.Xml.XmlReader.Create(arquivo);
                return (T)serializar.Deserialize(xmlArquivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
