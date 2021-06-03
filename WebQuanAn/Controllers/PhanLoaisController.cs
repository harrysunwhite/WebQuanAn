using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQuanAn.Models;

namespace WebQuanAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanLoaisController : ControllerBase
    {
        private readonly DataContext _context;

        public PhanLoaisController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PhanLoais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanLoai>>> GetPhanLoai()
        {
            return await _context.PhanLoai.ToListAsync();
        }

        // GET: api/PhanLoais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhanLoai>> GetPhanLoai(int id)
        {
            var phanLoai = await _context.PhanLoai.FindAsync(id);

            if (phanLoai == null)
            {
                return NotFound();
            }

            return phanLoai;
        }

        // PUT: api/PhanLoais/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhanLoai(int id, PhanLoai phanLoai)
        {
            if (id != phanLoai.Id)
            {
                return BadRequest();
            }

            _context.Entry(phanLoai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhanLoaiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PhanLoais
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PhanLoai>> PostPhanLoai(PhanLoai phanLoai)
        {
            _context.PhanLoai.Add(phanLoai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhanLoai", new { id = phanLoai.Id }, phanLoai);
        }

        // DELETE: api/PhanLoais/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhanLoai>> DeletePhanLoai(int id)
        {
            var phanLoai = await _context.PhanLoai.FindAsync(id);
            if (phanLoai == null)
            {
                return NotFound();
            }

            _context.PhanLoai.Remove(phanLoai);
            await _context.SaveChangesAsync();

            return phanLoai;
        }

        private bool PhanLoaiExists(int id)
        {
            return _context.PhanLoai.Any(e => e.Id == id);
        }
    }
}
