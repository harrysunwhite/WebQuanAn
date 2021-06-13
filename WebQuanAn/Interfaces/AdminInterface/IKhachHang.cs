

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Areas.Identity.Data;
using WebQuanAn.Models;
using X.PagedList;

namespace WebQuanAn.Interfaces
{
    public interface IKhachHang
    {
       
        AppUser Get(string id);
       
       
        IPagedList<AppUser> SearchByCondition(KhachHangSearchModel model);
         
    }
}



