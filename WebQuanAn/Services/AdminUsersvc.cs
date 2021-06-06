

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebQuanAn.Models;
using WebQuanAn.Interfaces;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using WebQuanAn.Helper;
using WebQuanAn.Models.ViewModels;

namespace WebQuanAn.Services
{
    public class AdminUsersvc : IAdminUser
    {
        private static int pageSize = 4;
        private readonly DataContext _context;
        private readonly IASMHelper _aSMHelper;
       
        public AdminUsersvc(DataContext context, IASMHelper aSMHelper)
        {
            _aSMHelper = aSMHelper;
            _context = context;

        }
        



        public int Add(AdminUser model)
        {
         
            model.MatKhau = _aSMHelper.Mahoa(model.MatKhau);
            try
            {

                
                    _context.Add(model);
                    _context.SaveChanges();
                    return model.Id;
              

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
                
            }     
        }

        public AdminUser Get(int id)
        {
           
            
                var item = _context.AdminUser.Find(id);

                if (item == null)
                {
                    return null;
                }
                return item;
            
              
        }

        public UpdateModel GetUpdate(int id)
        {
            return (from user in _context.AdminUser
                   where user.Id == id
                   select new UpdateModel
                   {
                       Id = user.Id,
                       Ho = user.Ho,
                       Ten = user.Ten,
                       Email = user.Email,
                       Hinh = user.Hinh,
                       Role = user.Role,
                       TrangThai = user.TrangThai,
                       SDT = user.SDT
                   }).FirstOrDefault();
        }    
        public int  Edit(UpdateModel model )
        {




            try
            {
                var adminUser = _context.AdminUser.Find(model.Id);
                adminUser.Ho = model.Ho;
                adminUser.Ten = model.Ten;
                adminUser.Email = model.Email;
                adminUser.Hinh = model.Hinh;
                adminUser.SDT = model.SDT;
                adminUser.Role = model.Role;
                adminUser.TrangThai = model.TrangThai;
                _context.Update(adminUser);
                _context.SaveChanges();
                return model.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }







        }

        public bool Delete(int Id)
        {
            try
            {
                
                    var find = _context.AdminUser.Find(Id);
                   

                    _context.AdminUser.Remove(find);
                    _context.SaveChanges();

                    return true;

                
            }   
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
         
        }

       
        public IPagedList<AdminUser> SearchByCondition(AdminUserSearchModel model)
        {

                IEnumerable<AdminUser> listUnpaged;
                listUnpaged = _context.AdminUser;

                           if(!string.IsNullOrWhiteSpace(model.Ten)) 
                                
                   {
                    listUnpaged = listUnpaged.Where(x => x.Ten.ToUpper().Contains(model.Ten.ToUpper()));
                   }
                        
                     
         

                if(!string.IsNullOrWhiteSpace(model.SDT)) 
                                
                   {
                    listUnpaged = listUnpaged.Where(x => x.SDT.ToUpper().Contains(model.SDT.ToUpper()));
                   }

           
                if (model.TrangThai == false) listUnpaged = listUnpaged.Where(x => x.TrangThai == true);
          










            var listPaged = listUnpaged.ToPagedList(model.Page ?? 1, pageSize);


                if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                    return null;

                return listPaged;

            


           
        }

        public AdminUser UserLogin(ViewLogin viewLogin)
        {
            try
            {
                return _context.AdminUser.Where(p => p.Email.Equals(viewLogin.Email) && p.MatKhau.Equals(_aSMHelper.Mahoa(viewLogin.Password))&& p.TrangThai).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }



        protected IEnumerable<AdminUser> GetAllFromDatabase()
        {
            List<AdminUser> data = new List<AdminUser>();
            
                data = _context.AdminUser.ToList();
                
            
            return data;
            
        }
        public bool CheckNewPass(string email,string NewPass)
        {
            string Oldpass = _context.AdminUser.Where(p => p.Email.Equals(email)).FirstOrDefault().MatKhau;
            if (string.Compare(_aSMHelper.Mahoa(NewPass), Oldpass, false) == 0) return true;
            else return false;

        }
        public bool CheckEmail(string email)
        {
            foreach(var user in _context.AdminUser)
            {
                if (string.Compare(user.Email, email, true) == 0) return false;
               
            }
            return true;
        }

        public bool ChangePass(ChangePassModel changePass)
        {
            var user = _context.AdminUser.Where(p => p.Email.Equals(changePass.UserEmail)).FirstOrDefault();
            try
            {
                user.MatKhau = _aSMHelper.Mahoa(changePass.NewPass);
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}


