using System;
using System.Collections.Generic;



namespace WebQuanAn.Models
{
    public partial class DonHang
    {
       

        public int MaDH { get; set; }
        public DateTime ThoiGian { get; set; }
        public byte TrangThai { get; set; }
        public int MaKH { get; set; }


        public string GhiChu { get; set; }
        public decimal TongTien { get; set; }

        public virtual KhachHang MaKhNavigation { get; set; }
        public virtual ICollection<CTHD> CTHDs { get; set; }
    }
}
