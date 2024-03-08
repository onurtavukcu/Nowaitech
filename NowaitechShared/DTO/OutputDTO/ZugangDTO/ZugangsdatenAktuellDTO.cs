﻿using NowaitechShared.Models;

namespace NowaiTechAPI.DTO.OutputDto.ZugangDTO
{
    public class ZugangsdatenAktuellDTO
    {
        [ColumnName("Zuständige Region")]
        public string? Zustandige_Region { get; set; }
        public string? SO_Nr { get; set; }  //saha numarası 
        public string? SO_Nr_EPlus { get; set; }

        [ColumnName("COOP-Contract")]
        public string? COOP_Contract { get; set; }

        [ColumnName("COOP-Status")]
        public string? COOP_Status { get; set; }

        [ColumnName("COOP-Tausch")]
        public string? COOP_Tausch { get; set; }
        public string? Infrastruktur_Soll_Baseframe_Radio_MW { get; set; }
        public string? Netzelement { get; set; }
        public string? Equipment_SO { get; set; }
        public string? Eigentümer { get; set; }

        [ColumnName("Gebäudeart")]
        public string? Gebaudeart { get; set; }
        public string? PLZ { get; set; }
        public string? Ort { get; set; }

        [ColumnName("Straße")]
        public string? Strabe { get; set; }

        [ColumnName("Rechtswert GK3")]
        public string? Rechtswert_GK3 { get; set; }

        [ColumnName("Hochwert GK3")]
        public string? Hochwert_GK3 { get; set; }

        [ColumnName("Östl Länge WGS84")]
        public string? Ostl_Lange_WGS84 { get; set; }

        [ColumnName("Nördl Breite WGS84")]
        public string? Nordl_Breite_WGS84 { get; set; }
        public string? Zugangsmöglichkeit { get; set; }
        public string? Zugangsregelung { get; set; }
        public string? Zufahrtsbeschreibung { get; set; }
        public string? Parkplätze { get; set; }
    }
}
