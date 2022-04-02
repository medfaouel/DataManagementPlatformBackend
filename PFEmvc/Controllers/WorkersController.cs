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
    public class WorkersController : ControllerBase
    {
        private readonly DbContextApp _context;

        public WorkersController(DbContextApp context)
        {
            _context = context;
        }
        [HttpGet("getWorkers")]
        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Workers.Include(env => env.Team).ToListAsync());
        }
        [HttpGet("getTeams")]
        public async Task<IActionResult> teams()
        {
            return Ok(await _context.Teams.ToListAsync());
        }



        [HttpGet("getWorkersById/{id}")]
        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateWorker worker)
        {
            if (ModelState.IsValid)


            {
                Worker wo = new();
                wo.UserName= worker.UserName;
                wo.LastName = worker.LastName;
                wo.FirstName = worker.FirstName;
                wo.Email = worker.Email;
                wo.Password = worker.Password;
                wo.LoginStatus = worker.LoginStatus;
                wo.Team = _context.Teams.First(ss => ss.TeamId == worker.teamId);

                _context.Add(wo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(worker);
        }
        
        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("UpdateWorker/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CreateWorker worker)
        {
            if ( !(_context.Workers.Where(wrk => wrk.UserId == id).ToList().Count() > 0))
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var wo = _context.Workers.First(worker => worker.UserId == id);
                    wo.UserName = worker.UserName; 
                    wo.LastName = worker.LastName;
                    wo.FirstName = worker.FirstName;
                    wo.Email = worker.Email;
                    wo.Password = worker.Password;
                    wo.LoginStatus = worker.LoginStatus;
                    wo.Team = _context.Teams.First(ss => ss.TeamId == worker.teamId);
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.UserId))
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
            return Ok(worker);
        }

        // POST: Workers/Delete/5
        [HttpDelete("DeleteWorker/{id}") ]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.UserId == id);
        }
    }
}
