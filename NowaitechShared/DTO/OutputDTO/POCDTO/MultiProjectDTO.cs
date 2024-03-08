using NowaitechShared.Models;

namespace NowaitechShared.DTO.OutputDTO.POCDTO
{
    public class MultiProjectDTO
    {
        public string? Order { get; set; }
        [ColumnName("Zuständige Region")]
        public string? Zuständige_Region { get; set; }
        public string? MP_Bezeichnung { get; set; }
        public int? Objekt_ID { get; set; } //project
        public string? Auftragnehmer_Integration_on_site { get; set; }
        public string? Generalunternehmer { get; set; }
        public string? Subunternehmer { get; set; }
    }
}
