

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Models;
using X.PagedList;

namespace WebQuanAn.Interfaces
{
    public interface IAdminUser
    {
        int Add(AdminUser model);
        AdminUser Get(int id);
        int Edit(AdminUser model);
        bool Delete(int Id);
        IPagedList<AdminUser> SearchByCondition(AdminUserSearchModel model);
    }
}



