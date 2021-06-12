

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
    public class ThucDonsvc : IThucDon
    {
        private static int pageSize = 4;
        private readonly DataContext _context;

        public ThucDonsvc(DataContext context)
        {
            _context = context;

        }
       

        
        
        public int Add(ThucDon model)
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

        public ThucDon Get(int id)
        {
            
                var item = _context.ThucDon.Find(id);

                if (item == null)
                {
                    return null;
                }
                return item;
            
              
        }
        public int  Edit(ThucDonUpdateModel model )
        {


           
               
                    try
                    {
                var thucDon = _context.ThucDon.Find(model.uId);
                thucDon.TenMon = model.uTenMon;
                thucDon.Mota = model.Mota;
                thucDon.Gia = model.uGia;
                thucDon.HinhAnh = model.uHinhAnh;
                thucDon.TrangThai = model.uTrangThai;
                thucDon.MaLoai = model.uMaLoai;

                        _context.Update(thucDon);
                        _context.SaveChanges();
                        return model.uId;
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
                
                    var find = _context.ThucDon.Find(Id);
                   

                    _context.ThucDon.Remove(find);
                    _context.SaveChanges();

                    return true;

                
            }   
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
         
        }
        public ThucDonUpdateModel GetUpdate(int id)
        {
            return (from td in _context.ThucDon
                    where td.Id == id
                    select new ThucDonUpdateModel
                    {
                        uId = td.Id,
                        uTenMon = td.TenMon,
                        Mota = td.Mota,
                        uGia = td.Gia,
                        uHinhAnh = td.HinhAnh,
                        uTrangThai = td.TrangThai,
                        uMaLoai = td.MaLoai,
                        uMaLoaiNavigation = td.MaLoaiNavigation
                    }).FirstOrDefault();
        }

       public IEnumerable<PhanLoai> PhanloaiNav()
        {
            return _context.PhanLoai.ToList();
        }
        public IPagedList<ThucDon> SearchByCondition(ThucDonSearchModel model)
        {

                IEnumerable<ThucDon> listUnpaged;
                listUnpaged = _context.ThucDon.Include(t=>t.MaLoaiNavigation);

                           if(!string.IsNullOrWhiteSpace(model.TenMon)) 
                                
                   {
                    listUnpaged = listUnpaged.Where(x => x.TenMon.ToUpper().Contains(model.TenMon.ToUpper()));
                   }




            if (model.Gia == 1) listUnpaged = listUnpaged.Where(x => x.Gia < 100000);
            if (model.Gia == 2) listUnpaged = listUnpaged.Where(x => x.Gia >= 100000 && x.Gia <= 300000);
            if (model.Gia == 3) listUnpaged = listUnpaged.Where(x => x.Gia > 300000);
            if (model.MaLoai != 0) listUnpaged = listUnpaged.Where(x => x.MaLoai == model.MaLoai);

                    if (model.TrangThai == false) listUnpaged = listUnpaged.Where(x => x.TrangThai == true);
                   
                
            

                   
                var listPaged = listUnpaged.ToPagedList(model.Page ?? 1, pageSize);


                if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                    return null;

                return listPaged;

            


           
        }



        protected IEnumerable<ThucDon> GetAllFromDatabase()
        {
            List<ThucDon> data = new List<ThucDon>();
            
                data = _context.ThucDon.Include(t => t.MaLoaiNavigation).ToList();
                
            
            return data;
            
        }
    }
}


