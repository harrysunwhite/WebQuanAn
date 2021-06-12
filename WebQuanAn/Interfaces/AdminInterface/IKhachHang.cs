

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Models;
using X.PagedList;

namespace WebQuanAn.Interfaces
{
    public interface IKhachHang
    {
        KhachHang Add(KhachHang model);
        KhachHang Get(Int32 id);
        KhachHang Edit(KhachHang model);
        bool Delete(Int32 Id);
        IPagedList<KhachHang> SearchByCondition(KhachHangSearchModel model);
         
    }
}



