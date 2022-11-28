using System;
using System.Collections.Generic;

namespace RepositoryLayer.Models
{
    public partial class HeaderSpr
    {
        public int Id { get; set; }
        public int? Proyek { get; set; }
        public string Peminta { get; set; }
        public DateTime? TanggalMinta { get; set; }
        public string LokasiPeminta { get; set; }
        public string KodeSpr { get; set; }
        public string TujuanSpr { get; set; }
        public string NamaPenyetuju1 { get; set; }
        public string NamaPenyetuju2 { get; set; }
        public bool? AdaDetil { get; set; }
        public bool? Status { get; set; }
    }
}
