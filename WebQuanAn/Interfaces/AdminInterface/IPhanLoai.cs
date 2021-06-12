

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Models;
using X.PagedList;

namespace WebQuanAn.Interfaces
{
    public interface IPhanLoai
    {
        int Add(PhanLoai model);
        PhanLoai Get(int id);
        int Edit(PhanLoai model);
        bool Delete(int Id);
        IPagedList<PhanLoai> SearchByCondition(PhanLoaiSearchModel model);
    }
}



