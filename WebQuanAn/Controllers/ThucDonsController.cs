using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebQuanAn.Models;

namespace WebQuanAn.Controllers
{
    public class ThucDonsController : Controller
    {
        private readonly DataContext _context;

        public ThucDonsController(DataContext context)
        {
            _context = context;
        }

        // GET: ThucDons
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.ThucDon.Include(t => t.MaLoaiNavigation);
            return View(await dataContext.ToListAsync());
        }

        // GET: ThucDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thucDon = await _context.ThucDon
                .Include(t => t.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thucDon == null)
            {
                return NotFound();
            }

            return View(thucDon);
        }

        // GET: ThucDons/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.PhanLoai, "Id", "TenLoai");
            return View();
        }

        // POST: ThucDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenMon,Mota,Gia,HinhAnh,TrangThai,MaLoai")] ThucDon thucDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thucDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.PhanLoai, "Id", "Id", thucDon.MaLoai);
            return View(thucDon);
        }

        // GET: ThucDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thucDon = await _context.ThucDon.FindAsync(id);
            if (thucDon == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.PhanLoai, "Id", "Id", thucDon.MaLoai);
            return View(thucDon);
        }

        // POST: ThucDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenMon,Mota,Gia,HinhAnh,TrangThai,MaLoai")] ThucDon thucDon)
        {
            if (id != thucDon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thucDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThucDonExists(thucDon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.PhanLoai, "Id", "Id", thucDon.MaLoai);
            return View(thucDon);
        }

        // GET: ThucDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thucDon = await _context.ThucDon
                .Include(t => t.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thucDon == null)
            {
                return NotFound();
            }

            return View(thucDon);
        }

        // POST: ThucDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thucDon = await _context.ThucDon.FindAsync(id);
            _context.ThucDon.Remove(thucDon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThucDonExists(int id)
        {
            return _context.ThucDon.Any(e => e.Id == id);
        }
    }
}
