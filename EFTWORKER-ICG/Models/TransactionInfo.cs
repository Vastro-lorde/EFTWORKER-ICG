using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EFTWORKER_ICG.Models
{
    [XmlRoot("Info")]
    public class TransactionInfo
    {
        [XmlElement("TipoTx")]
        public string TipoTx { get; set; }

        [XmlElement("NumTicket")]
        public int NumTicket { get; set; }

        [XmlElement("SerieTicket")]
        public string SerieTicket { get; set; }

        [XmlElement("NumLinPago")]
        public int NumLinPago { get; set; }

        [XmlElement("NumTicketOrig")]
        public int NumTicketOrig { get; set; }

        [XmlElement("CodCajera")]
        public int CodCajera { get; set; }

        [XmlElement("NomCajera")]
        public string NomCajera { get; set; }

        [XmlElement("EsSupervisor")]
        public string EsSupervisor { get; set; }

        [XmlElement("Importe")]
        public decimal Importe { get; set; }

        [XmlElement("NumDec")]
        public int NumDec { get; set; }

        [XmlElement("FP")]
        public int FP { get; set; }

        [XmlElement("Caja")]
        public string Caja { get; set; }

        [XmlElement("FechaOp")]
        public string FechaOp { get; set; }

        [XmlElement("HoraOp")]
        public string HoraOp { get; set; }

        [XmlElement("DescMoneda")]
        public string DescMoneda { get; set; }

        [XmlElement("CodMoneda")]
        public int CodMoneda { get; set; }

        [XmlElement("ICGTipoApp")]
        public string ICGTipoApp { get; set; }

        [XmlElement("ImpuestosIncluidos")]
        public string ImpuestosIncluidos { get; set; }

        [XmlElement("CodCliente")]
        public int CodCliente { get; set; }

        [XmlElement("AliasCliente")]
        public string AliasCliente { get; set; }

        [XmlArray("Lineas")]
        [XmlArrayItem("Registro")]
        public List<TransactionLine> Lineas { get; set; }

        [XmlElement("IdTransaccion")]
        public int IdTransaccion { get; set; }

        [XmlElement("AceptaPagoParcial")]
        public string AceptaPagoParcial { get; set; }

        [XmlElement("FechaTrans")]
        public string FechaTrans { get; set; }

        [XmlElement("EmailCliente")]
        public string EmailCliente { get; set; }
    }

    public class TransactionLine
    {
        [XmlElement("CodArticulo")]
        public int CodArticulo { get; set; }

        [XmlElement("Descripcion")]
        public string Descripcion { get; set; }

        [XmlElement("Unidades")]
        public int Unidades { get; set; }

        [XmlElement("Precio")]
        public decimal Precio { get; set; }

        [XmlElement("Importe")]
        public decimal Importe { get; set; }

        [XmlElement("Impuesto")]
        public decimal Impuesto { get; set; }

        [XmlElement("ImporteIva")]
        public decimal ImporteIva { get; set; }

        [XmlElement("CodBarras")]
        public long CodBarras { get; set; }

        [XmlElement("NoDtoAplicable")]
        public bool NoDtoAplicable { get; set; }
    }
}
