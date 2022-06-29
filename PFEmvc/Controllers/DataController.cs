using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFEmvc;
using PFEmvc.dto;
using PFEmvc.Extensions;
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
            return Ok(await _context.Data.Include(data => data.Check).ToListAsync());
        }
        [HttpGet("getChecks")]
        // GET: Workers
        public async Task<IActionResult> Checks()
        {
            return Ok(await _context.Checks.ToListAsync());
        }
        [HttpGet("getCriterias")]
        // GET: Workers
        public async Task<IActionResult> Criterias()
        {
            return Ok(await _context.Criterias.ToListAsync());
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
        public async Task<IActionResult> CreateData([FromBody] createData data)
        {
            if (ModelState.IsValid)
            {
                Data d = data.ToEntity();
                _context.Add(d);
                await _context.SaveChangesAsync();
                List<Criterias> criterias = _context.Criterias.ToList();
         
                check check = new()
                {
                    CheckAddress = "Need to be filled",
                    Status = "Not Passed",
                    CheckDetails = criterias.Select(criteria => new CheckDetails()
                    {
                        Criteria = criteria,
                        CDQM_comments = "Need to be filled",
                        CDQM_feedback = "Need to be filled",
                        DQMS_feedback = "Need to be filled",
                        TopicOwner_feedback = "Need to be filled",
                    }).ToList(),
                    Data = new List<Data>()
                        {
                            d
                        }
                };

                _context.Add(check);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(data);
        }
        [HttpPost("CreateDataFromExcel")]
        public async Task<IActionResult> CreateDataFromExcel([FromBody] CreateDataFromExcel data)
        {
            var dataList = data.CreatedDataFromExcel.Select(d => d.ToEntity()).ToList();
            _context.AddRange(dataList);
            await _context.SaveChangesAsync();
            dataList.ForEach(data =>
            {
                List<Criterias> criterias = _context.Criterias.ToList();
               
                check check = new()
                {
                    CheckAddress = "Need to be filled",
                    Status = "Not Passed",
                    CheckDetails = criterias.Select(criteria => new CheckDetails()
                    {
                        Criteria = criteria,
                        CDQM_comments = "Need to be filled",
                        CDQM_feedback = "Need to be filled",
                        DQMS_feedback = "Need to be filled",
                        TopicOwner_feedback = "Need to be filled",
                        status= "Not Passed",
                    }).ToList(),
                    Data = new List<Data>()
                        {
                            data
                        }
                };

                _context.Add(check);
                _context.SaveChanges();
            });

            return Ok();
        }
        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("UpdateData/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] createData data)
        {
            if (!(_context.Data.Where(wrk => wrk.DataId == id).ToList().Count() > 0))
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    Data d = _context.Data.FirstOrDefault(aa => aa.DataId == id);
                    d.Context = data.Context;
                    d.Fors_Material_Group = data.Fors_Material_Group;
                    d.LEONI_Part = data.LEONI_Part;
                    d.LEONI_Part_Classification = data.LEONI_Part_Classification;
                    d.Month = data.Month;
                    d.Part_Request = data.Part_Request;
                    d.Supplier = data.Supplier;
                    _context.Update(d);
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
