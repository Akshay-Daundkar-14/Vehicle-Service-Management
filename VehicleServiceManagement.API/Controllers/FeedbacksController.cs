using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepository _service;

        public FeedbacksController(IFeedbackRepository service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            var feedbackDomain = await _service.GetAllFeedbackAsync();
            if (feedbackDomain == null)
            {
                return NotFound();
            }
           
            return Ok(feedbackDomain);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback([FromRoute] int id)
        {
            if (_service.GetAllFeedbackAsync == null || id <= 0)
            {
                return NotFound();
            }
            var feedback = await _service.GetFeedbackAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback([FromRoute]int id, [FromBody]Feedback feedback)
        {
            if (id != feedback.FeedbackId || feedback == null)
            {
                return BadRequest();
            }

            try
            {
              await  _service.UpdateFeedbackAsync(feedback);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetFeedbackAsync(id) ==null)
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


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Feedback>> PostFeedback([FromBody] Feedback feedback)
        {
            if (_service.GetAllFeedbackAsync == null || feedback == null)
            {
                return Problem("Entity set 'AppDbContext.Feedbacks'  is null.");
            }

           await _service.CreateFeedbackAsync(feedback);

            return CreatedAtAction("GetFeedback", new { id = feedback.FeedbackId }, feedback);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback([FromRoute]int id)
        {
            if (_service.GetAllFeedbackAsync == null || id <=0)
            {
                return NotFound();
            }
            var feedback = await _service.GetFeedbackAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

           await _service.DeleteFeedbackAsync(feedback);

            return NoContent();
        }

       
    }
}
