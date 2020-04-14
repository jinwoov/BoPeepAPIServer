﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBackEnd.Data;
using APIBackEnd.Models;

namespace APIBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagActivitiesController : ControllerBase
    {
        private readonly BoPeepDbContext _context;

        public TagActivitiesController(BoPeepDbContext context)
        {
            _context = context;
        }

        // GET: api/TagActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagActivity>>> GetTagActivity()
        {
            return await _context.TagActivity.ToListAsync();
        }

        // GET: api/TagActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagActivity>> GetTagActivity(int id)
        {
            var tagActivity = await _context.TagActivity.FindAsync(id);

            if (tagActivity == null)
            {
                return NotFound();
            }

            return tagActivity;
        }

        // PUT: api/TagActivities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTagActivity(int id, TagActivity tagActivity)
        {
            if (id != tagActivity.ActivitiesId)
            {
                return BadRequest();
            }

            _context.Entry(tagActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagActivityExists(id))
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

        // POST: api/TagActivities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TagActivity>> PostTagActivity(TagActivity tagActivity)
        {
            _context.TagActivity.Add(tagActivity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TagActivityExists(tagActivity.ActivitiesId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTagActivity", new { id = tagActivity.ActivitiesId }, tagActivity);
        }

        // DELETE: api/TagActivities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TagActivity>> DeleteTagActivity(int id)
        {
            var tagActivity = await _context.TagActivity.FindAsync(id);
            if (tagActivity == null)
            {
                return NotFound();
            }

            _context.TagActivity.Remove(tagActivity);
            await _context.SaveChangesAsync();

            return tagActivity;
        }

        private bool TagActivityExists(int id)
        {
            return _context.TagActivity.Any(e => e.ActivitiesId == id);
        }
    }
}