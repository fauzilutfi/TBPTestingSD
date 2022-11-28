using System;
using System.Collections.Generic;

namespace RepositoryLayer.Models
{
    public partial class DetilSpr
    {
        public int Id { get; set; }
        public int? IdRef { get; set; }
        public int? Material { get; set; }
        public DateTime? TanggalRencanaDiterima { get; set; }
        public bool? StatusDisetujui { get; set; }
        public string Unit { get; set; }
        public string Volume { get; set; }
    }
}
