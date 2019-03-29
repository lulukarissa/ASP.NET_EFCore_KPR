using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kpr_project.Models
{
    public partial class SkemaDetail
    {
        [Key]
        public Guid Iddetail { get; set; }
        public int Bulan { get; set; }
        public decimal Pokok { get; set; }
        public decimal Bunga { get; set; }
        public decimal Pelunasanpokok { get; set; }
        public decimal Tagihan { get; set; }
        public decimal Sisapokok { get; set; }

        public Guid Idskema { get; set; }

        public SkemaKpr SkemaKpr { get; set; }
    }
}