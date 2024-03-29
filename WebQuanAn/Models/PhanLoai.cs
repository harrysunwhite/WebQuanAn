﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebQuanAn.Models
{
    public partial class PhanLoai
    {
    [Key]
        public  Int32  Id { get; set; }
        [Display(Name ="Tên loại thực đơn")]
        [Required(ErrorMessage ="Nhập tên loại")]
        [StringLength(100)]
        public  String  TenLoai { get; set; }
    }
    public class PhanLoaiSearchModel
    {

        public int? Page { get; set; }


        public String TenLoai { get; set; }
    }
}


