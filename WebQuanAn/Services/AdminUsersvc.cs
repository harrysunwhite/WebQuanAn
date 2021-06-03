

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

namespace WebQuanAn.Services
{
    public class AdminUsersvc : IAdminUser
    {
        private static int pageSize = 6;
        private readonly DataContext _context;

        public AdminUsersvc(DataContext context)
        {
            _context = context;

        }
       

        
        
        public int Add(AdminUser model)
        {
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
        public int  Edit(AdminUser model )
        {
           
               

               
                    try
                    {
                        _context.Update(model);
                        _context.SaveChanges();
                        return model.Id;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        Console.WriteLine(ex);
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
                        
                     
         

                if(!string.IsNullOrWhiteSpace(model.TrangThai.ToString())) 
                                
                    {
                     listUnpaged = listUnpaged.Where(x => x.TrangThai==model.TrangThai);
                    }
          

                   
                var listPaged = listUnpaged.ToPagedList(model.Page ?? 1, pageSize);


                if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                    return null;

                return listPaged;

            


           
        }



        protected IEnumerable<AdminUser> GetAllFromDatabase()
        {
            List<AdminUser> data = new List<AdminUser>();
            
                data = _context.AdminUser.ToList();
                
            
            return data;
            
        }
    }
}


