using System;
using System.Collections.Generic;

namespace RepositoryLayer.Models
{
    public partial class Material
    {
        public int Id { get; set; }
        public string Material1 { get; set; }
        public bool? TipeMaterial { get; set; }
    }
}
