using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebQuanAn.Models;

namespace WebQuanAn.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public enum Gender
    {
        [Display(Name = "Nam")]
        Nam = 1,
        [Display(Name = "Nữ")]
        Nu = 2
    }

    public class AppUser : IdentityUser
    {
       
        [PersonalData]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }
        [PersonalData]
        [Display(Name = "Giới tính")]
        public Gender GioiTinh { get; set; }
        [PersonalData]
        [Display(Name = "Ngày sinh")]
        public DateTime NgaySinh { get; set; }
      
        [PersonalData]
        [Display(Name = "Link Facebook")]
        public string FacebookLink { get; set; }
        


        public virtual ICollection<DonHang> DonHangs { get; set; }
      
    }
}
