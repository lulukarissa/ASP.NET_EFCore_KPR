using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace kpr_project.Models
{
    public partial class kprContext : DbContext
    {
        public kprContext()
        {
        }

        public kprContext(DbContextOptions<kprContext> options)
            : base(options)
        {
        }
        
        public DbSet<SkemaKpr> SkemaKpr { get; set; }
        public DbSet<SkemaDetail> SkemaDetail { get; set; }

    }
}
