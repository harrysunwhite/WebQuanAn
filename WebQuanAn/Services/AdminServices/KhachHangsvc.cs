

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
using WebQuanAn.Areas.Identity.Data;

namespace WebQuanAn.Services
{
    public class KhachHangsvc : IKhachHang
    {
        private static int pageSize = 6;
        private readonly DataContext _context;

        public KhachHangsvc(DataContext context)
        {
            _context = context;

        }
       

        
        
      

        public AppUser Get(string id)
        {
            
                var item = _context.KhachHang.Find(id);

                if (item == null)
                {
                    return null;
                }
                return item;
            
              
        }
           
       
        public IPagedList<AppUser> SearchByCondition(KhachHangSearchModel model)
        {

                IEnumerable<AppUser> listUnpaged;
                listUnpaged = _context.KhachHang  ;
               
                

                if(!string.IsNullOrWhiteSpace(model.Name)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.Name.ToUpper().Contains(model.Name.ToUpper()));
                   }
                         
                                  
                                                                                                          if(!string.IsNullOrWhiteSpace(model.Sdt)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.PhoneNumber.ToUpper().Contains(model.Sdt.ToUpper()));
                   }
                         
                                  
                                                                        if(!string.IsNullOrWhiteSpace(model.Email)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.Email.ToUpper().Contains(model.Email.ToUpper()));
                   }
                         
                                  
                                                                                                                                                 
              
                        
             
               
                var listPaged = listUnpaged.ToPagedList(model.Page ?? 1, pageSize);


                if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                    return null;

                return listPaged;

            


           
        }



        protected IEnumerable<AppUser> GetAllFromDatabase()
        {
            List<AppUser> data = new List<AppUser>();
            
                data = _context.KhachHang.ToList();
                
            
            return data;
            
        }
    }
}


