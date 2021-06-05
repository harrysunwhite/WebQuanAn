using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Required(ErrorMessage = "Họ không được để trống")]
        [StringLength(50)]
        public string Ho { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50)]
        public string Ten { get; set; }
        
        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Remote(action: "ValidateEmail", controller: "adminuser")]
        public string Email { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        
        public string MatKhau { get; set; }
        [NotMapped]
        [Display(Name = "Mật khẩu xác nhận")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("MatKhau",ErrorMessage ="Mật khẩu không trùng khơp")]
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


    public class ChangePassModel
    {
        public string UserEmail { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu cũ")]
        [DataType(DataType.Password)]
       
       

        public string Oldpass { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        public string NewPass { get; set; }
        [DataType(DataType.Password)]

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("NewPass",ErrorMessage ="Mật khẩu không trùng khớp")]
       
        public string ConfirmPass { get; set; }
    }


    public class UpdateModel
    {
        [Display(Name = "Mã Admin")]
        public int Id { get; set; }

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "Họ không được để trống")]
        [StringLength(50)]
        public string Ho { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
       
        public string Email { get; set; }
       

        [Display(Name = "Hình ảnh")]
        public string Hinh { get; set; }
        [NotMapped]
        public IFormFile FileHinh { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"((09|03|07|08|05)+([0-9]{8})\b)", ErrorMessage = "Invail phone number")]
        [StringLength(15)]
        public string SDT { get; set; }


      
        [Range(1, 2, ErrorMessage = "Chọn quyền quản trị")]
        public Role Role { get; set; }


        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
    }
}
