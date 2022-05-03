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
        //aa
        private readonly DbContextApp _context;

        public checksController(DbContextApp context)
        {
            _context = context;
        }
        [HttpGet("getChecks")]
        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Checks.Include(env => env.environment).Include(env => env.Criterias).Include(env => env.Data).ToListAsync());
        }
        [HttpGet]
        [Route("getChecksDetails")]
        public IEnumerable<CheckDetails> getChecksDetails()
        {
            return _context.CheckDetails;
        }
        [HttpPost("CreateChecksDetails")]
        public  IActionResult CreateChecksDetails([FromBody] CheckDetails details)
        {
            if (details ==null)
            {
                return BadRequest();

            }
            else
            {
                _context.CheckDetails.Add(details);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message ="added successfully"
                });
            }
        }
        // For Student Detail with studentid to load by student ID  
        // GET api/values/5  
        [HttpGet]
        [Route("getChecksDetails/{id}")]
        public IEnumerable<CheckDetails> getChecksDetails(int id)
        {
            return _context.CheckDetails.Where(i => i.CheckDetailId == id).ToList();
        }
        [HttpGet("getEnvs")]
        public async Task<IActionResult> Env()
        {
            return Ok(await _context.Environments.ToListAsync());
        }
        [HttpGet("getData")]
        public async Task<IActionResult> Data()
        {
            return Ok(await _context.Data.ToListAsync());
        }
        [HttpGet("getCriterias")]
        public async Task<IActionResult> Criterias()
        {
            return Ok(await _context.Criterias.ToListAsync());
        }



        [HttpGet("getCheckById/{id}")]
        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _context.Checks.Include(c => c.Criterias).Include(c=>c.Data).Include(c => c.environment)
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
                ch.CheckAddress = check.CheckAddress;
                ch.CDQM_comments = check.CDQM_comments;
                ch.CDQM_feedback = check.CDQM_feedback;
                ch.DQMS_feedback = check.DQMS_feedback;
                ch.TopicOwner_feedback = check.TopicOwner_feedback;

                ch.Status = check.status;
                ch.environment = _context.Environments.First(aa => aa.EnvId == check.envId);
                ch.Criterias = new();
                if (check.CriteriaIds is not null)
                {

                for (int i = 0; i < check.CriteriaIds.Count; i++)
                {
                    var criteria = _context.Criterias.First(cr => cr.CrtId == check.CriteriaIds[i]
                    );

                    ch.Criterias.Add(criteria);

                }
                }
                ch.Data = new();
                if (check.DataId != 0)
                {

                ch.Data = _context.Data.First(aa => aa.DataId == check.DataId);
                }


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
                    ch.CheckAddress = check.CheckAddress;
                    
                    ch.Status = check.status;
                    ch.CDQM_comments = check.CDQM_comments;
                    ch.CDQM_feedback = check.CDQM_feedback;
                    ch.DQMS_feedback = check.DQMS_feedback;
                    ch.TopicOwner_feedback = check.TopicOwner_feedback;
                    ch.environment = _context.Environments.First(aa => aa.EnvId == check.envId);
                    ch.Criterias = new();
                    if (check.CriteriaIds is not null)
                    {

                        for (int i = 0; i < check.CriteriaIds.Count; i++)
                    {
                        var criteria = _context.Criterias.First(cr =>
                            cr.CrtId == check.CriteriaIds[i]
                        );
                        ch.Criterias.Add(criteria);
                    }
                    }
                    

                        ch.Data =ch.Data = _context.Data.First(aa => aa.DataId == check.DataId);
                    

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
