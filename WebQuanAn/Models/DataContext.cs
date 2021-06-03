

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



namespace WebQuanAn.Models
{
    public class DataContext : DbContext
    {
      

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<PhanLoai> PhanLoai { get; set; }
        public DbSet<ThucDon> ThucDon { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<ThucDon>(entity =>
            {
                

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.ThucDons)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThucDon__MaLoai__1CF15040");
            });

          
        }

       


    }
}


