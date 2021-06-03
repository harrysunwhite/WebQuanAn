

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
        public  String  TenMon { get; set; }
        public  String  Mota { get; set; }
        public  Decimal  Gia { get; set; }
        public  String  HinhAnh { get; set; }
        public  Boolean  TrangThai { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public  Int32  MaLoai { get; set; }

        public virtual PhanLoai MaLoaiNavigation { get; set; }
    }
    public partial class PhanLoai
    {
        public virtual ICollection<ThucDon> ThucDons { get; set; }
    }
}


