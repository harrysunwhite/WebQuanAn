

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
        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<CTHD> CTHD { get; set; }
        public DbSet<DonHang> DonHang { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.ToTable("AdminUser");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Hinh)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Ho)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SDT)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CTHD>(entity =>
            {
                entity.HasKey(e => new { e.MaDh, e.MaTd });
                    

                entity.ToTable("CTDH");

                entity.Property(e => e.MaDh).HasColumnName("MaDH");

                entity.Property(e => e.MaTd).HasColumnName("MaTD");

                entity.HasOne(d => d.MaDhNavigation)
                    .WithMany(p => p.CTHDs)
                    .HasForeignKey(d => d.MaDh)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MaTdNavigation)
                    .WithMany(p => p.CTHDs)
                    .HasForeignKey(d => d.MaTd)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                    
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.MaDH);
                   

                entity.ToTable("DonHang");

               
                    

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.MaKH).HasColumnName("MaKH");

                entity.Property(e => e.ThoiGian).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaKH)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                    
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.ToTable("KhachHang");

               

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FacebookLink).HasMaxLength(250);

                entity.Property(e => e.Ho)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MatKhau).IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
                    

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PhanLoai>(entity =>
            {
                entity.ToTable("PhanLoai");

                entity.Property(e => e.TenLoai)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ThucDon>(entity =>
            {
                entity.ToTable("ThucDon");

                entity.Property(e => e.Gia).HasColumnType("money");

                entity.Property(e => e.HinhAnh).HasMaxLength(250);

                entity.Property(e => e.TenMon)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.ThucDons)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                    
            });

            
        }

     


    }
}


