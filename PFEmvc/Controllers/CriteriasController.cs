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
    public class CriteriasController : Controller
    {
        private readonly DbContextApp _context;

        public CriteriasController(DbContextApp context)
        {
            _context = context;
        }
        [HttpGet("getCriterias")]
        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Criterias.Include(env => env.environment).ToListAsync());
        }
        [HttpGet("getEnvs")]
        public async Task<IActionResult> Env()
        {
            return Ok(await _context.Environment.ToListAsync());
        }


        [HttpGet("getCriteriaById/{id}")]
        // GET: Criterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Criteria = await _context.Criterias
                .FirstOrDefaultAsync(m => m.CrtId == id);
            if (Criteria == null)
            {
                return NotFound();
            }

            return Ok(Criteria);
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("AddCriteria")]
        public async Task<IActionResult> Create([FromBody] LookingForEnvInCriterias Criteria)
        {
            if (ModelState.IsValid)
            {
                Criterias crt = new();
                crt.Description = Criteria.description;
                crt.Name = Criteria.name;
                crt.environment = _context.Environment.First(cr => cr.EnvId == Criteria.envId);
                _context.Add(crt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(Criteria);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("UpdateCriteria/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] LookingForEnvInCriterias Criteria)
        {
            if (!(_context.Criterias.Where(wrk => wrk.CrtId == id).ToList().Count() > 0))
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var crt = _context.Criterias.First(re => re.CrtId == id);
                    crt.Description = Criteria.description;
                    crt.Name = Criteria.name;
                    crt.environment = _context.Environment.First(aa => aa.EnvId == Criteria.envId);
                    _context.Update(crt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriteriaExists(Criteria.crtId))
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
            return Ok(Criteria);
        }

        // POST: Workers/Delete/5
        [HttpDelete("DeleteCriteria/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Criteria = await _context.Criterias.FindAsync(id);
            _context.Criterias.Remove(Criteria);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CriteriaExists(int id)
        {
            return _context.Criterias.Any(e => e.CrtId == id);
        }
    }
}
