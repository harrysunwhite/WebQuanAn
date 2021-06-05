

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Models;
using X.PagedList;

namespace WebQuanAn.Interfaces
{
    public interface IThucDon
    {
        int Add(ThucDon model);
        ThucDon Get(int id);
        int Edit(ThucDon model);
        bool Delete(int Id);
        
        IEnumerable<PhanLoai> PhanloaiNav();
        IPagedList<ThucDon> SearchByCondition(ThucDonSearchModel model);
    }
}



