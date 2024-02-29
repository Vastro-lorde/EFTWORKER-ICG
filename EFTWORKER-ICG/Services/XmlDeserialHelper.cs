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
            try
            {
                if (xmlString == null || xmlString == "")
                {
                    throw new ArgumentNullException("xmlString should not be empty");
                }
                XmlSerializer serializer = new(typeof(TransactionInfo));
                using StringReader reader = new(xmlString);
                return (TransactionInfo)serializer.Deserialize(reader);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public string SerializeXmlStringSuccess(bool status,string? cardBin, string? cardBrand, string? transactionId, string errCode, string errorDescription)
        {
            return $"<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>\r\n<Info>\r\n<Respuesta>T</Respuesta>\r\n<IdTransaccion>{transactionId}</IdTransaccion>\r\n<BinTarjeta>{cardBin}</BinTarjeta>\r\n<MarcaTarjeta>{cardBrand}</MarcaTarjeta>\r\n</Info>";
        }

        public string SerializeXmlStringFail(string errCode, string errorDescription)
        {
            return $"<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>\r\n<Info>\r\n<Respuesta>F</Respuesta>\r\n<Error>\r\n<CodError>{errCode}</CodError>\r\n<DescError>{errorDescription}</DescError>\r\n</Error>\r\n</Info>";
        }

        public string SerializeRequestString(decimal amount, string invoiceNumber, string serieNumber, string transactionId)
        {
            return $@"<?xml version=""1.0"" encoding=""ISO-8859-1"" ?>
                    <Info>
                        <Amount>{amount}</Amount>
                        <InvoiceNumber>{invoiceNumber}</InvoiceNumber>
                        <Serie>{serieNumber}</Serie>
                        <TransactionId>{transactionId}</TransactionId>
                    </Info>";
        }

    }
}
