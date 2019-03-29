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
    [ApiController]
    public class kprController : ControllerBase
    {
        private readonly kprContext _context;

        public kprController(kprContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkemaKpr>>> GetSkemaKpr()
        {
            return await _context.SkemaKpr.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{Idskema}")]
        public async Task<ActionResult<SkemaKpr>> GetSkemaId(Guid Idskema)
        {
            var SkemaKpr = await _context.SkemaKpr.FindAsync(Idskema);

            if (SkemaKpr == null)
            {
                return NotFound();
            }

            return SkemaKpr;
        }

        // POST api/<controller>
        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<SkemaKpr>> PostSkemaKpr(SkemaKpr data)
        {
            _context.SkemaKpr.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkemaKpr), new { Id = data.Idskema }, data);
        }
    }
}
