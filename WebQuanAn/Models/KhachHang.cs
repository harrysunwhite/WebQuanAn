using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebQuanAn.Models
{
    public enum Gender
    {
        [Display(Name ="Nam")]
        Nam = 1,
        [Display(Name ="Nữ")]
        Nu = 2
    }
    public partial class KhachHang
    {
      
        public int Id { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public Gender GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string FacebookLink { get; set; }
        public string MatKhau { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
