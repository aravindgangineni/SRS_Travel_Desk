﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;

namespace TravelDesk_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HR Travel Admin")]
    public class TravelAdminController : ControllerBase
    {
        private readonly TravelDeskContext _context;

        public TravelAdminController(TravelDeskContext context)
        {
            _context = context;
        }

        // View a history of all travel requests
        [HttpGet("all-requests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var allRequests = await _context.TravelRequests
                                            .Include(tr => tr.Comments)
                                            .Include(tr => tr.User)
                                            .OrderByDescending(tr => tr.RequestId)
                                            .ToListAsync();

            return Ok(allRequests);
        }

        // Perform an action on a travel request
        [HttpPost("action-request/{id}")]
        public async Task<IActionResult> ActionRequest(int id, [FromBody] TravelAdminActionDto actionDto)
        {
            var travelAdminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (travelAdminIdClaim == null) return Unauthorized("Travel Admin ID not found in token.");
            int travelAdminId = int.Parse(travelAdminIdClaim.Value);

            var request = await _context.TravelRequests
                                        .FirstOrDefaultAsync(tr => tr.RequestId == id);

            if (request == null)
            {
                return NotFound("Request not found.");
            }

            // A comment is required for every action
            if (string.IsNullOrEmpty(actionDto.Comments))
            {
                return BadRequest("The comments section cannot be left blank.");
            }

            var newComment = new RequestComment
            {
                Comment = actionDto.Comments,
                RequestId = id,
                UserId = travelAdminId,
                Timestamp = DateTime.UtcNow
            };
            _context.RequestComments.Add(newComment);

            switch (actionDto.Action.ToLower())
            {
                case "book ticket":
                    request.Status = "Completed";
                    request.PassportFileUrl = actionDto.TicketFileUrl; // Or a dedicated field for tickets
                    break;
                case "return to manager":
                    request.Status = "Returned to Manager";
                    break;
                case "return to employee":
                    request.Status = "Returned to Employee";
                    break;
                case "close":
                    request.Status = "Completed";
                    break;
                default:
                    return BadRequest("Invalid action specified.");
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = $"Request {id} status updated to {request.Status}." });
        }
    }
}