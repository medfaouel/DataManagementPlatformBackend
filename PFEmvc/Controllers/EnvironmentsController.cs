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
    public class EnvironmentsController : Controller
    {
        private readonly DbContextApp _context;

        public EnvironmentsController(DbContextApp context)
        {
            _context = context;
        }
        [HttpGet("getEnvironments")]
        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Environments.Include(env => env.Teams).Include(env => env.Checks).ToListAsync());
        }
        [HttpGet("getTeams")]
        public async Task<IActionResult> Env()
        {
            return Ok(await _context.Teams.ToListAsync());
        }
        
        [HttpGet("getChecks")]
        // GET: Workers
        public async Task<IActionResult> Checks()
        {
            return Ok(await _context.Checks.ToListAsync());
        }




        [HttpGet("getEnvironmentById/{id}")]
        // GET: Criterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Criteria = await _context.Environments
                .FirstOrDefaultAsync(m => m.EnvId == id);
            if (Criteria == null)
            {
                return NotFound();
            }

            return Ok(Criteria);
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("AddEnvironment")]
        public async Task<IActionResult> Create([FromBody] CreateEnv env)
        {

            if (ModelState.IsValid)
            {
                Models.Environment envir = new();
                envir.Description = env.Description;
                envir.EnvName = env.EnvName;
                envir.Teams = new();
                for (int i = 0; i < env.teamIds.Count; i++)
                {
                    var team = _context.Teams.First(team =>  team.TeamId == env.teamIds[i]
                    );
                    
                    envir.Teams.Add(team);

                }
                
                envir.Checks = new();
                for (int i = 0; i < env.ChecksIds.Count; i++)
                {
                    var check = _context.Checks.First(cr => cr.CheckId == env.ChecksIds[i]
                    );

                    envir.Checks.Add(check);

                }



                _context.Add(envir);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(env);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("UpdateEnvironment/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CreateEnv env)
        {
            if (!(_context.Environments.Where(wrk => wrk.EnvId == id).ToList().Count() > 0))
            {
                return NotFound();

            }


            if (ModelState.IsValid)
            {
                try
                {
                    
                    var envir = _context.Environments.First(en => en.EnvId == id);
                    envir.Description = env.Description;
                    envir.EnvName = env.EnvName;
                    envir.Teams = new();
                    for (int i = 0; i < env.teamIds.Count; i++)
                    {
                        var team = _context.Teams.First(team =>
                            team.TeamId == env.teamIds[i]
                        );
                        envir.Teams.Add(team);
                    }
                   
                    envir.Checks = new();
                    for (int i = 0; i < env.ChecksIds.Count; i++)
                    {
                        var check = _context.Checks.First(cr =>
                            cr.CheckId == env.ChecksIds[i]
                        );
                        envir.Checks.Add(check);
                    }

                    _context.Update(envir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentExists(env.EnvId))
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
            return Ok(env);
        }

        // POST: Workers/Delete/5
        [HttpDelete("DeleteEnvironment/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var env = await _context.Environments.FindAsync(id);
            _context.Environments.Remove(env);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EnvironmentExists(int id)
        {
            return _context.Environments.Any(e => e.EnvId == id);
        }
    }
}
