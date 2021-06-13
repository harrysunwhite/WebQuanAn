using System;
using System.Collections.Generic;



namespace WebQuanAn.Models
{
    public partial class CTHD
    {
        public string MaDh { get; set; }
        public int MaTd { get; set; }
        public int SoLuong { get; set; }

        public virtual DonHang MaDhNavigation { get; set; }
        public virtual ThucDon MaTdNavigation { get; set; }
    }
}
