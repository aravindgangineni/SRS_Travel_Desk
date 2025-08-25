using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;

namespace TravelDesk_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly TravelDeskContext _context;

        public ManagerController(TravelDeskContext context)
        {
            _context = context;
        }

        // View assigned requests
        [HttpGet("my-requests")]
        public async Task<IActionResult> GetAssignedRequests()
        {
            var managerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (managerIdClaim == null) return Unauthorized("Manager ID not found in token.");
            int managerId = int.Parse(managerIdClaim.Value);

            var assignedRequests = await _context.TravelRequests
                                                 .Include(tr => tr.Comments)
                                                 .Include(tr => tr.User)
                                                 .Where(tr => tr.User.ManagerId == managerId)
                                                 .ToListAsync();

            return Ok(assignedRequests);
        }

        // Perform an action on a request
        [HttpPost("action-request/{id}")]
        public async Task<IActionResult> ActionRequest(int id, [FromBody] ManagerActionDto actionDto)
        {
            var managerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (managerIdClaim == null) return Unauthorized("Manager ID not found in token.");
            int managerId = int.Parse(managerIdClaim.Value);

            var request = await _context.TravelRequests
                                        .Include(tr => tr.User)
                                        .FirstOrDefaultAsync(tr => tr.RequestId == id);

            if (request == null || request.User.ManagerId != managerId)
            {
                return NotFound("Request not found or not assigned to this manager.");
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
                UserId = managerId,
                Timestamp = DateTime.UtcNow
            };
            _context.RequestComments.Add(newComment);

            switch (actionDto.Action.ToLower())
            {
                case "approve":
                    request.Status = "Approved";
                    // Note: An email notification to HR Travel Admin should be triggered here
                    break;
                case "disapprove":
                    request.Status = "Disapproved";
                    break;
                case "return to employee":
                    request.Status = "Returned to Employee";
                    break;
                default:
                    return BadRequest("Invalid action specified.");
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = $"Request {id} status updated to {request.Status}." });
        }
    }
}