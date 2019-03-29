using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kpr_project.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kpr_project.Controllers
{
    [Route("api/[controller]")]
    public class kprdetailController : Controller
    {
        private readonly kprContext _context;

        public kprdetailController(kprContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkemaDetail>>> GetSkemaDetail()
        {
            return await _context.SkemaDetail.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{Idskema}")]
        public async Task<ActionResult<SkemaDetail>> GetSkemaId(Guid Idskema)
        {
            var SkemaDetail = await _context.SkemaDetail.FindAsync(Idskema);

            if (SkemaDetail == null)
            {
                return NotFound();
            }

            return SkemaDetail;
        }

        // POST api/<controller>
        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<SkemaDetail>> PostSkemaDetail(SkemaDetail data)
        {
            _context.SkemaDetail.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkemaDetail), new { Id = data.Iddetail }, data);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
