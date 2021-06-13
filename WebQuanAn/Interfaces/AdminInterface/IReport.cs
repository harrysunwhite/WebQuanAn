using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Models;
using X.PagedList;

namespace WebQuanAn.Interfaces
{
    public interface IReport
    {
        DonHang Get(string id);
        List<CTHD> GetCTHDs(string id);
        DonHang Edit(DonHang model);
        IPagedList<DonHang> SearchByCondition(DonHangSearchModel model);
    }
}
