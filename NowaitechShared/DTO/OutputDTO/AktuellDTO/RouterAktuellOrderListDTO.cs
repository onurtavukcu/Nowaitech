using NowaitechShared.Models;

namespace NowaitechShared.DTO.OutputDTO.AktuellDTO
{
    public class RouterAktuellOrderListDTO
    {
        public string? CtK_Status { get; set; }
        public string? NE_Nr { get; set; }
        public string? Alt { get; set; }
        public string? NE_Name { get; set; }
        public string? NE_Bemerkung { get; set; }
        public string? Projektart { get; set; }
        [ColumnName("Projekt-ID")]
        public string? Projekt_ID { get; set; }
        [ColumnName("SC-Kategorie")]
        public string? SC_Kategorie { get; set; }
        [ColumnName("SC-Nummer")]
        public string? SC_Nummer { get; set; }
    }
}
