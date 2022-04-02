using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFEmvc;
using PFEmvc.Models;

namespace PFEmvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly DbContextApp _context;

        public DataController(DbContextApp context)
        {
            _context = context;
        }
        [HttpGet("getData")]
        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Data.ToListAsync());
        }




        [HttpGet("getDataById/{id}")]
        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _context.Data
                .FirstOrDefaultAsync(m => m.DataId == id);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("CreateData")]
        public async Task<IActionResult> Create([FromBody] Data data)
        {
            if (ModelState.IsValid)
            {
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(data);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("UpdateData/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Data data)
        {
            if (!(_context.Data.Where(wrk => wrk.DataId == id).ToList().Count() > 0))
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataExists(data.DataId))
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
            return Ok(data);
        }

        // POST: Workers/Delete/5
        [HttpDelete("DeleteData/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _context.Data.FindAsync(id);
            _context.Data.Remove(data);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool DataExists(int id)
        { 
            return _context.Data.Any(e => e.DataId == id);
        }
    }
}
