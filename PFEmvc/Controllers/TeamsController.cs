using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFEmvc;
using PFEmvc.dto;
using PFEmvc.Models;
using WebApplicationPFE.Models;

namespace PFEmvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly DbContextApp _context;
        private readonly UserManager<AppUser> _userManager;
        public TeamsController(DbContextApp context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet("getTeams")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Teams.Include(env => env.environment).Include(env => env.Users).Include(team => team.criterias).ToListAsync());
        }
        [HttpGet("getEnvs")]
        public async Task<IActionResult> Env()
        {
            return Ok(await _context.Environments.ToListAsync());
        }
        [HttpGet("getWorkers")]
        // GET: Workers
        public async Task<IActionResult> Workers()
        {
            return Ok(await _context.Workers.ToListAsync());
        }
        [HttpGet("getCriterias")]
        // GET: Workers
        public async Task<IActionResult> Criterias()
        {
            return Ok(await _context.Criterias.ToListAsync());
        }

        [HttpGet("getTeamById/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpPost("AddTeam")]
        public async Task<IActionResult> Create([FromBody] CreateTeam teamenv)
        {
            if (ModelState.IsValid)
            {
                Team team = new();
                team.TeamDescription = teamenv.teamDescription;
                team.TeamName = teamenv.teamName;
                
                team.environment = _context.Environments.FirstOrDefault(envir => envir.EnvId == teamenv.envId);
                team.Users = new();
                for (int i = 0; i < teamenv.userIds.Count; i++)
                {
                    var test = _userManager.Users;
                    var users = _userManager.Users.FirstOrDefault(user => user.Id == teamenv.userIds[i]
                    ); 
                    team.Users.Add(users);

                }
                



                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(teamenv);
        }

        [HttpPut("UpdateTeam/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CreateTeam team)
        {
            if (!(_context.Teams.Where(wrk => wrk.TeamId == id).ToList().Count() > 0))
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var te = _context.Teams.First(en => en.TeamId == id);
                    te.TeamDescription = team.teamDescription;
                    te.TeamName = team.teamName;
                    te.environment = _context.Environments.First(aa => aa.EnvId == team.envId);

                    te.Users = new();
                    if (team.userIds is not null)
                        for (int i = 0; i < team.userIds.Count; i++)
                        {
                            var users = _context.Users.First(worker => worker.Id == team.userIds[i].ToString()
                            );

                            te.Users.Add(users);

                        }
                    te.criterias = new();
                    for (int i = 0; i < team.criteriaIds.Count; i++)
                    {
                        var criteria = _context.Criterias.First(criteria => criteria.CrtId == team.criteriaIds[i]
                        );

                        te.criterias.Add(criteria);

                    }


                    _context.Update(te);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.teamId))
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
            return Ok(team);
        }

        // POST: Teams/Delete/5
        [HttpDelete("DeleteTeam/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }

}
