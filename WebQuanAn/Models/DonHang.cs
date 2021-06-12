﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebQuanAn.Models
{
   public enum TrangThai
    {
        
        [Display(Name = "Chờ xác nhận")]
         _1= 1,
        [Display(Name = "Đang Giao")]
        _2 = 2,
        [Display(Name = "Hoàn thành")]
        _3 = 3,
        [Display(Name = "Đã Huỷ")]
        _4 = 4,
    }
    public partial class DonHang
    {
       

        public int MaDH { get; set; }
        public DateTime ThoiGian { get; set; }
        public TrangThai TrangThai { get; set; }
        public int MaKH { get; set; }

        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
     
        public virtual KhachHang MaKhNavigation { get; set; }
        public virtual ICollection<CTHD> CTHDs { get; set; }
    }

    public class DonHangSearchModel
    {
        public int? Page { get; set; }
        public string TenKH { get; set; }
        public string SDT { get; set; }
        public DateTime? NgayHD { get; set; }
        public int TrangThai { get; set; }
    }
}
