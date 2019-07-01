using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemeGenerator.Models;

namespace MemeGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly memyContext _context;

        public APIController(memyContext context)
        {
            _context = context;
        }

        // GET: api/API
     
      

        [HttpGet]
        public async Task<ActionResult<List<Object>>> Memyyy(int likee)
        {
          // var me = _context.Memy.OrderBy(s => s.Like);
            var mm = _context.Memy
                    .Select(i => new { i.Like, i.Dislike, i.coverImg })
                    .OrderBy(i=>i.Like);

            var v2 = await mm.Take(2).ToListAsync();
            return v2.ConvertAll( x => (Object)x);
        }

        // GET: api/API/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var memy = await _context.Memy.FindAsync(id);

            if (memy == null)
            {
                return NotFound();
            }

            return Ok(memy);
        }

        // PUT: api/API/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemy([FromRoute] int id, [FromBody] Memy memy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != memy.Id_mema)
            {
                return BadRequest();
            }

            _context.Entry(memy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemyExists(id))
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

        // POST: api/API
        [HttpPost]
        public async Task<IActionResult> PostMemy([FromBody] Memy memy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Memy.Add(memy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemy", new { id = memy.Id_mema }, memy);
        }

        // DELETE: api/API/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var memy = await _context.Memy.FindAsync(id);
            if (memy == null)
            {
                return NotFound();
            }

            _context.Memy.Remove(memy);
            await _context.SaveChangesAsync();

            return Ok(memy);
        }

        private bool MemyExists(int id)
        {
            return _context.Memy.Any(e => e.Id_mema == id);
        }
    }
}