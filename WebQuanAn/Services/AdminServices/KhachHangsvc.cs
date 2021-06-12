

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
    public class KhachHangsvc : IKhachHang
    {
        private static int pageSize = 6;
        private readonly DataContext _context;

        public KhachHangsvc(DataContext context)
        {
            _context = context;

        }
       

        
        
        public KhachHang Add(KhachHang model)
        {
            try
            {

                
                    _context.Add(model);
                    _context.SaveChanges();
                    return model;
              

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
                
            }     
        }

        public KhachHang Get(Int32 id)
        {
            
                var item = _context.KhachHang.Find(id);

                if (item == null)
                {
                    return null;
                }
                return item;
            
              
        }
        public KhachHang  Edit(KhachHang model )
        {
           
               

               
                    try
                    {
                        _context.Update(model);
                        _context.SaveChanges();
                        return model;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        Console.WriteLine(ex);
                        return null;
                    }
                    
              
                

          
            
           
        }

       public bool Delete(Int32 Id)
        {
            try
            {
                
                    var find = _context.KhachHang.Find(Id);
                   

                    _context.KhachHang.Remove(find);
                    _context.SaveChanges();

                    return true;

                
            }   
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
         
        }
                
       
        public IPagedList<KhachHang> SearchByCondition(KhachHangSearchModel model)
        {

                IEnumerable<KhachHang> listUnpaged;
                listUnpaged = _context.KhachHang  ;
               
                

                if(!string.IsNullOrWhiteSpace(model.Name)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.Name.ToUpper().Contains(model.Name.ToUpper()));
                   }
                         
                                  
                                                                                                          if(!string.IsNullOrWhiteSpace(model.Sdt)) 
                                
                   {
                     listUnpaged = listUnpaged.Where(x => x.Sdt.ToUpper().Contains(model.Sdt.ToUpper()));
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



        protected IEnumerable<KhachHang> GetAllFromDatabase()
        {
            List<KhachHang> data = new List<KhachHang>();
            
                data = _context.KhachHang.ToList();
                
            
            return data;
            
        }
    }
}


