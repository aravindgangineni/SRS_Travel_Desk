using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;

namespace TravelDesk_Api.Controllers
{
    // A Data Transfer Object to handle incoming manager actions
    public class ManagerActionDto
    {
        public string Action { get; set; }
        public string Comments { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly TravelDeskContext _context;
        private readonly EmailService _emailService;

        public ManagerController(TravelDeskContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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

                    // Get the manager's details from the database
                    var manager = await _context.Users.FindAsync(managerId);

                    // Get HR email and send notification
                    var hrAdmin = await _context.Users.FirstOrDefaultAsync(u => u.RoleId == 2);
                    if (hrAdmin != null && manager != null)
                    {
                        var subject = $"Travel Request {id} Approved by Manager";
                        var body = $"Travel request (ID: {request.RequestId}) from {request.EmployeeName} has been approved by {manager.FirstName} {manager.LastName}. Please review and take action.";
                        await _emailService.SendEmailAsync(hrAdmin.Email, subject, body);
                    }
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