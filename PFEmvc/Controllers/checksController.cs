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
            return Ok(await _context.Checks
                   
                .Include(env => env.Data)
                .Include(env => env.CheckDetails)
                .ThenInclude(details => details.Criteria)
                .ToListAsync());
        }
        [HttpGet]
        [Route("getChecksDetails")]
        public IEnumerable<CheckDetails> getChecksDetails()
        {
            return _context.CheckDetails;
        }
        [HttpPut("FillCheckDetails/{id}")]
        public async Task<IActionResult> FillCheckDetails(int id, [FromBody] FillMasterDetailsChecks fill )
        {
            if (!(_context.CheckDetails.Where(wrk => wrk.CheckDetailId == id).ToList().Count() > 0))
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var ch = _context.CheckDetails.First(aa => aa.CheckDetailId == id);

                    ch.CDQM_comments = fill.CDQM_comments;
                    ch.CDQM_feedback = fill.CDQM_feedback;
                    ch.DQMS_feedback = fill.DQMS_feedback;
                    ch.TopicOwner_feedback = fill.TopicOwner_feedback;

                    _context.Update(ch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!checkExists(fill.CheckDetailId))
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
            return Ok(fill);
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
            return _context.CheckDetails.Where(i => i.CheckId == id).ToList();
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

            var check = await _context.Checks.Include(c=>c.Data)
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

                ch.Status = "en train de";
                ch.Data = new();
                for (int i = 0; i < check.DataIds.Count; i++)
                {
                    var data = _context.Data.FirstOrDefault(team => team.DataId == check.DataIds[i]
                    );
                    if (data is not null){ 

                        ch.Data.Add(data);
                    }

                }


                _context.Add(ch);
                await _context.SaveChangesAsync();
                foreach (var criteria in _context.Criterias)
                {
                    CheckDetails checkDetails = new()
                    {
                        CDQM_comments = "A remplir",
                        CDQM_feedback = "A remplir",
                        DQMS_feedback = "A remplir",
                        status = "A remplir",
                        TopicOwner_feedback = "A remplir",
                        Criteria = criteria,
                        CheckId = ch.CheckId
                    };
                    _context.Add(checkDetails);
                }
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
            if (!(_context.Checks.Where(wrk => wrk.CheckId == id    ).ToList().Count() > 0))
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
                    for (int i = 0; i < check.DataIds.Count; i++)
                    {
                        var data = _context.Data.First(team => team.DataId == check.DataIds[i]
                        );

                        ch.Data.Add(data);

                    }               
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
