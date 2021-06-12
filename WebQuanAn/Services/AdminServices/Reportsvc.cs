using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Interfaces;
using WebQuanAn.Models;
using X.PagedList;

namespace WebQuanAn.Services
{
    public class Reportsvc:IReport
    {
        private static int pageSize = 6;
        private readonly DataContext _context;

        public Reportsvc(DataContext context)
        {
            _context = context;

        }

        public DonHang Get(int id)
        {

            var item = _context.DonHang.Find(id);

            if (item == null)
            {
                return null;
            }
            return item;
        }
        public List<CTHD> GetCTHDs(int id)
        {
            var Listitem = _context.CTHD.Where(d=>d.MaDh == id).Include(i=>i.MaTdNavigation).Include(i=>i.MaDhNavigation).ToList();

            if (Listitem == null)
            {
                return null;
            }
            return Listitem;
        }


        public DonHang Edit(DonHang model)
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

        public IPagedList<DonHang> SearchByCondition(DonHangSearchModel model)
        {

            IEnumerable<DonHang> listUnpaged;
            listUnpaged = _context.DonHang.Include(t => t.MaKhNavigation).OrderByDescending(i=>i.ThoiGian);

            if (!string.IsNullOrWhiteSpace(model.TenKH))

            {
                listUnpaged = listUnpaged.Where(x => x.MaKhNavigation.Name.ToUpper().Contains(model.TenKH.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(model.SDT))

            {
                listUnpaged = listUnpaged.Where(x => x.MaKhNavigation.Sdt.ToUpper().Contains(model.SDT.ToUpper()));
            }

            if(!string.IsNullOrWhiteSpace(model.TrangThai.ToString())&&model.TrangThai!=0)
            {
                listUnpaged = listUnpaged.Where(x => x.TrangThai == (TrangThai)model.TrangThai);
            }

            if (!string.IsNullOrWhiteSpace(model.NgayHD.ToString()))

            {
                listUnpaged = listUnpaged.Where(x => x.MaKhNavigation.Sdt.ToUpper().Contains(model.SDT.ToUpper()));
            }





            var listPaged = listUnpaged.ToPagedList(model.Page ?? 1, pageSize);


            if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                return null;

            return listPaged;

        }
    }
}
