using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kpr_project.Models
{
    public partial class SkemaKpr
    {
        [Key]
        public Guid Idskema { get; set; }
        public decimal Harga { get; set; }
        public decimal Dp { get; set; }
        public decimal Bunga { get; set; }
        public int Tenor { get; set; }
    }
}
