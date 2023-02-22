using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Entities;

namespace TennisTournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefereesController : ControllerBase
    {
        private readonly TennisContext _context;

        public RefereesController(TennisContext context)
        {
            _context = context;
        }

        // GET: api/Referees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Referee>>> GetReferees()
        {
            return await _context.Referees.ToListAsync();
        }

        // GET: api/Referees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Referee>> GetReferee(int id)
        {
            var referee = await _context.Referees.FindAsync(id);

            if (referee == null)
            {
                return NotFound();
            }

            return referee;
        }

        // PUT: api/Referees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferee(int id, Referee referee)
        {
            if (id != referee.ID)
            {
                return BadRequest();
            }

            _context.Entry(referee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefereeExists(id))
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

        // POST: api/Referees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Referee>> PostReferee(Referee referee)
        {
            _context.Referees.Add(referee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReferee", new { id = referee.ID }, referee);
        }

        // DELETE: api/Referees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferee(int id)
        {
            var referee = await _context.Referees.FindAsync(id);
            if (referee == null)
            {
                return NotFound();
            }

            _context.Referees.Remove(referee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefereeExists(int id)
        {
            return _context.Referees.Any(e => e.ID == id);
        }
    }
}
