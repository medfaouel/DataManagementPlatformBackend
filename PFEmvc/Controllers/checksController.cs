using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFEmvc;
using PFEmvc.dto;
using PFEmvc.Models;
using PFEmvc.Models.Enums;

namespace PFEmvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class checksController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DbContextApp _context;

        public checksController(DbContextApp context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet("getChecks")]
        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Checks

                .Include(env => env.Data)
                .Include(env => env.CheckDetails)
                .ThenInclude(details => details.Criteria)
                .ThenInclude(details =>details.Team)

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
        [HttpPut("FillAllCheckDetailsById/{id}")]
        public async Task<IActionResult> FillAllCheckDetailsById(int id, [FromBody] FillAllMasterDetailsChecks fills)
        {
            if (!(_context.Checks.Where(wrk => wrk.CheckId == id).ToList().Count() > 0))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    fills.FillMasterDetailsChecks.ToList().ForEach(fill =>
                    {
                        var checkDetails = _context.CheckDetails.Where(x => x.CheckDetailId == fill.CheckDetailId ).FirstOrDefault();
                        if(checkDetails is not null)
                        {
                            checkDetails.CDQM_comments = fill.CDQM_comments;
                            checkDetails.CDQM_feedback = fill.CDQM_feedback;
                            checkDetails.TopicOwner_feedback = fill.TopicOwner_feedback;
                            checkDetails.DQMS_feedback = fill.DQMS_feedback;
                            checkDetails.status = fill.Status;
                            _context.Update(checkDetails);
                            _context.SaveChanges();
                        }
                    });
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        throw;
                    
                }
                return NoContent();
            }
            return Ok();
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
        [HttpGet("getAllChecksDetails")]
        public async Task<IActionResult> getAllChecksDetails()
        {
            return Ok(await _context.CheckDetails.ToListAsync());
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
            return Ok(await _context.Criterias.Include(c => c.Team).ToListAsync());
        }
        [HttpPost("SendEmailToTopicOwner")]
        public async Task<IActionResult> SendEmailToTopicOwner(idDTO model)
        {
            if (model.idCheck == null || model.idCheckDetails == null)
            {
                return NotFound();
            }
            var data = _context.Data.Include(c => c.Check).FirstOrDefault(x => x.Check.CheckId == model.idCheck);
            var checkdetail = _context.CheckDetails.Include(c => c.Criteria).FirstOrDefault(x => x.CheckDetailId == model.idCheckDetails);
            var NeededUserEmail = "";
            var users = _userManager.Users.Where(x => x.Team.TeamId == model.teamId).ToList();
            foreach (var user in users)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                if (role == "TopicOwner")
                {
                    NeededUserEmail = user.Email;

                }
                //var test = _userManager.Users.Include(a => a.Team).FirstOrDefault(x => x.Id == user.Id);

            }

            EmailSender.SendEmailToTopicOwner(data.LEONI_Part, checkdetail.Criteria.Name, NeededUserEmail);
            return Ok();
        }

        [HttpGet("getAllCheckdetailsByCheckId/{id}")]
        // GET: Workers/Details/5
        public async Task<IActionResult> getAllCheckdetailsByCheckId(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkDetails = _context.CheckDetails
               .OrderBy(team => team.CheckId).Include(c => c.Criteria).Include(c => c.Check).Include(c => c.Criteria.Team).Where(team =>team.CheckId==id);
                

            

           // var check = await _context.CheckDetails.Include(c => c.Criteria).Include(c => c.Check)
            //    .FirstOrDefaultAsync(m => m.CheckDetailId == id);
            //if (check == null)
           // {
            //    return NotFound();
            //}

            return Ok(checkDetails);
        }
        [HttpGet("getAllCheckdetailsByTeamId/{idCheck}/{idTeam}")]
       
        public async Task<IActionResult> getAllCheckdetailsByTeamIdAndCheckId(int? idCheck,int? idTeam)
        {
            if (idCheck == null || idTeam==null)
            {
                return NotFound();
            }

            var checkDetails = _context.CheckDetails
               .OrderBy(team => team.CheckId).Include(c => c.Criteria).Include(c => c.Criteria.Team).Where(team => team.Criteria.Team.TeamId == idTeam).Where(team => team.CheckId == idCheck);


            return Ok(checkDetails);
        }
        [HttpGet("getAllChecksByTeamId/{idTeam}")]
        
        public async Task<IActionResult> getAllChecksByTeamId(int idTeam)
        {
            if ( idTeam == null)
            {
                return NotFound();
            }

            var checks = _context.Checks.Include(x => x.CheckDetails).ThenInclude(x => x.Criteria).ThenInclude(x => x.Team).ToList();
            var neededChecks= checks.Where(x => x.CheckDetails.Any(y => y.Criteria.Team.TeamId == idTeam));
            List<NeededStatusOfChecks> list = neededChecks.Select(x => new check { });
            return Ok();
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

                ch.Status = "Not Passed";
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
                        CDQM_comments = "Need to be filled",
                        CDQM_feedback = "Need to be filled",
                        DQMS_feedback = "Need to be filled",
                        status = "Need to be filled",
                        TopicOwner_feedback = "Need to be filled",
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
                    if(check.DataIds is not null){
                        for (int i = 0; i < check.DataIds.Count; i++)
                        {
                            var data = _context.Data.First(team => team.DataId == check.DataIds[i]
                            );

                            ch.Data.Add(data);

                        }
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
