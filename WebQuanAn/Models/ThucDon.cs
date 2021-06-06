

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebQuanAn.Models
{
   
    public class ThucDon
    {
    [Key]
        public  Int32  Id { get; set; }

        [Display(Name = "Tên món ăn")]
        [Required(ErrorMessage = "Nhập tên món ăn")]
        [StringLength(100)]
        public  String  TenMon { get; set; }
        [Display(Name = "Mô tả")]
       
        public  String  Mota { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Nhập Giá")]
       
        public  Decimal  Gia { get; set; }

        [Display(Name = "Hình ảnh")]
       
        public  String  HinhAnh { get; set; }

        [Display(Name = "Đang phục vụ")]
       
        public  Boolean  TrangThai { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Loại thức ăn")]
        public  Int32  MaLoai { get; set; }

        
       
        public virtual PhanLoai MaLoaiNavigation { get; set; }
        public ICollection<CTHD> CTHDs { get; set; }
    }
    public partial class PhanLoai
    {
        public virtual ICollection<ThucDon> ThucDons { get; set; }
    }

    public class ThucDonSearchModel
    {

        public int? Page { get; set; }


        public String TenMon { get; set; }

        public Decimal? Gia { get; set; }
        public int MaLoai { get; set; }
        public bool TrangThai { get; set; }
    }
}


