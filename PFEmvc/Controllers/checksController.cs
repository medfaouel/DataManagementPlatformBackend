using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFEmvc;
using PFEmvc.dto;
using PFEmvc.Models;

namespace PFEmvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class checksController : Controller
    {
        private readonly DbContextApp _context;

        public checksController(DbContextApp context)
        {
            _context = context;
        }
        [HttpGet("getChecks")]
        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Checks.Include(env => env.environment).ToListAsync());
        }
        [HttpGet("getEnvs")]
        public async Task<IActionResult> Env()
        {
            return Ok(await _context.Environment.ToListAsync());
        }



        [HttpGet("getChecksById/{id}")]
        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _context.Checks
                .FirstOrDefaultAsync(m => m.CheckId == id);
            if (check == null)
            {
                return NotFound();
            }

            return Ok(check);
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] LookingForEnvInCheck check)
        {
            if (ModelState.IsValid)
            {
                check ch = new();
                ch.Comments = check.comments;
                ch.Status = check.status;
                ch.environment = _context.Environment.First(aa => aa.EnvId == check.envId);
                _context.Add(ch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(check);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("UpdateCheck/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] LookingForEnvInCheck check)
        {
            if (!(_context.Checks.Where(wrk => wrk.CheckId == id).ToList().Count() > 0))
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var ch = _context.Checks.First(aa => aa.CheckId == id);
                    ch.Comments = check.comments;
                    ch.Status = check.status;
                    ch.environment = _context.Environment.First(aa => aa.EnvId == check.envId);
                    _context.Update(ch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!checkExists(check.checkId))
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
            return Ok(check);
        }

        // POST: Workers/Delete/5
        [HttpDelete("DeleteCheck/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var check = await _context.Checks.FindAsync(id);
            _context.Checks.Remove(check);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool checkExists(int id)
        {
            return _context.Checks.Any(e => e.CheckId == id);
        }
    }
}
