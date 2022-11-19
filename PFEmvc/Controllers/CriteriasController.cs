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
            return Ok(await _context.Criterias.Include(crt => crt.Team).ToListAsync());
        }
        [HttpGet("getTeams")]
        public async Task<IActionResult> team()
        {
            return Ok(await _context.Teams.ToListAsync());
        }
        [HttpGet("getCheck")]
        public async Task<IActionResult> Check()
        {
            return Ok(await _context.Checks.ToListAsync());
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
                
                crt.Team = _context.Teams.FirstOrDefault(cr => cr.TeamId == Criteria.TeamId);
                _context.Add(crt);
                await _context.SaveChangesAsync();
                foreach (var check in _context.Checks)
                {
                    CheckDetails checkDetails = new()
                    {
                        CDQM_comments = "Need to be filled",
                        CDQM_feedback = "Need to be filled",
                        DQMS_feedback = "Need to be filled",
                        status = "Not Passed",
                        TopicOwner_feedback = "Need to be filled",
                        Criteria = crt,
                        CheckId = check.CheckId
                    };
                    _context.Add(checkDetails);
                }
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

                    
                    crt.Team = _context.Teams.First(cr => cr.TeamId == Criteria.TeamId);
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
