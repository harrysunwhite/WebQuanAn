using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebQuanAn.Models
{
    public enum Role
    {
        [Display(Name ="Quản lý")]
        Admin = 1,
        [Display(Name ="User")]
        User = 2
    }
    public partial class AdminUser
    {
        [Display(Name ="Mã Admin")]
        public int Id { get; set; }

        [Display(Name ="Họ")]
        [Required(ErrorMessage ="This field is required")]
        [StringLength(50)]
        public string Ho { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string Ten { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string MatKhau { get; set; }
        [NotMapped]
        [Display(Name = "Mật khẩu xác nhận")]
        [Required(ErrorMessage = "This field is required")]
        [Compare("MatKhau",ErrorMessage ="Password not matched")]
        public string MatkhauCF { get; set; }

        [Display(Name ="Hình ảnh")]
        public string Hinh { get; set; }
        [NotMapped]
        public IFormFile FileHinh { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"((09|03|07|08|05)+([0-9]{8})\b)", ErrorMessage = "Invail phone number")]
        [StringLength(15)]
        public string SDT { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [Range(1,2,ErrorMessage ="Chọn quyền quản trị")]
        public Role Role { get; set; }


        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
    }

    public class AdminUserSearchModel
    {

        public int? Page { get; set; }


        public String Ten { get; set; }

        public String SDT { get; set; }

        public Boolean TrangThai { get; set; }
    }
}
