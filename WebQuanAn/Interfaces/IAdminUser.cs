

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Models;
using WebQuanAn.Models.ViewModels;
using X.PagedList;

namespace WebQuanAn.Interfaces
{
    public interface IAdminUser
    {
        int Add(AdminUser model);
        AdminUser Get(int id);
        UpdateModel GetUpdate(int id);
        int Edit(UpdateModel model);
        bool Delete(int Id);
        IPagedList<AdminUser> SearchByCondition(AdminUserSearchModel model);
        AdminUser UserLogin(ViewLogin viewLogin);
        bool CheckEmail(string email);
        bool CheckNewPass(string email, string Oldpass);

        bool ChangePass(ChangePassModel changePassModel);
        
        


    }
}



