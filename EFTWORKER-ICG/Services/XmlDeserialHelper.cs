using EFTWORKER_ICG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EFTWORKER_ICG.Services
{
    public class XmlDeserialHelper
    {
        public TransactionInfo ParseXmlString(string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TransactionInfo));
            using (StringReader reader = new StringReader(xmlString))
            {
                return (TransactionInfo)serializer.Deserialize(reader);
            }
        }

        public string SerializeXmlString(bool status,string? cardBin, string? cardBrand, string? transactionId)
        {
            string requesta = string.Empty;
            if (status)
            {
                requesta = "T";
                return $"<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>\r\n<Info>\r\n<Respuesta>{requesta}</Respuesta>\r\n<IdTransaccion>{transactionId}</IdTransaccion>\r\n<BinTarjeta>{cardBin}</BinTarjeta>\r\n<MarcaTarjeta>{cardBrand}</MarcaTarjeta>\r\n</Info>";
            }
            else
            {
                requesta = "F";
                return $"<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>\r\n<Info>\r\n<Respuesta>{requesta}</Respuesta>\r\n<IdTransaccion>{transactionId}</IdTransaccion>\r\n<BinTarjeta>{cardBin}</BinTarjeta>\r\n<MarcaTarjeta>{cardBrand}</MarcaTarjeta>\r\n</Info>";
            }
            
        }
    }
}
