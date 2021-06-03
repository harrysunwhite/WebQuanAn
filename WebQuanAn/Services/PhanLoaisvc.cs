

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
    public class PhanLoaisvc : IPhanLoai
    {
        private static int pageSize = 6;
        private readonly DataContext _context;

        public PhanLoaisvc(DataContext context)
        {
            _context = context;

        }
       

        
        
        public int Add(PhanLoai model)
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

        public PhanLoai Get(int id)
        {
            
                var item = _context.PhanLoai.Find(id);

                if (item == null)
                {
                    return null;
                }
                return item;
            
              
        }
        public int  Edit(PhanLoai model )
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
                
                    var find = _context.PhanLoai.Find(Id);
                   

                    _context.PhanLoai.Remove(find);
                    _context.SaveChanges();

                    return true;

                
            }   
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
         
        }

       
        public IPagedList<PhanLoai> SearchByCondition(PhanLoaiSearchModel model)
        {

                IEnumerable<PhanLoai> listUnpaged;
                listUnpaged = _context.PhanLoai;

                           if(!string.IsNullOrWhiteSpace(model.TenLoai)) 
                                
                   {
                    listUnpaged = listUnpaged.Where(x => x.TenLoai.ToUpper().Contains(model.TenLoai.ToUpper()));
                   }
                        
                     
         

                   
                var listPaged = listUnpaged.ToPagedList(model.Page ?? 1, pageSize);


                if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                    return null;

                return listPaged;

            


           
        }



        protected IEnumerable<PhanLoai> GetAllFromDatabase()
        {
            List<PhanLoai> data = new List<PhanLoai>();
            
                data = _context.PhanLoai.ToList();
                
            
            return data;
            
        }
    }
}


