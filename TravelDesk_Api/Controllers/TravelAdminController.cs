using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TravelDesk_Api.Controllers
{
    // DTO for incoming HR Travel Admin actions
    public class TravelAdminActionDto
    {
        public string Action { get; set; }
        public string Comments { get; set; }
        public string? TicketFileUrl { get; set; } // Dummy URL from Postman
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HR Travel Admin")]
    public class TravelAdminController : ControllerBase
    {
        private readonly TravelDeskContext _context;
        private readonly EmailService _emailService;

        public TravelAdminController(TravelDeskContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // View all requests
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
            if (travelAdminIdClaim == null)
                return Unauthorized("Travel Admin ID not found in token.");

            int travelAdminId = int.Parse(travelAdminIdClaim.Value);

            var request = await _context.TravelRequests
                                        .Include(tr => tr.User)
                                        .ThenInclude(u => u.Manager)
                                        .FirstOrDefaultAsync(tr => tr.RequestId == id);

            if (request == null)
            {
                return NotFound("Request not found.");
            }

            if (string.IsNullOrEmpty(actionDto.Comments))
            {
                return BadRequest("The comments section cannot be left blank.");
            }

            // Save comment in DB
            var newComment = new RequestComment
            {
                Comment = actionDto.Comments,
                RequestId = id,
                UserId = travelAdminId,
                Timestamp = DateTime.UtcNow
            };
            _context.RequestComments.Add(newComment);

            var oldStatus = request.Status;

            switch (actionDto.Action.ToLower())
            {
                case "book ticket":
                    request.Status = "Completed";

                    // Auto-generate folder structure & file
                    var storageRoot = "C:\\travel_desk_docs";  // Base storage folder
                    var employeeFolder = Path.Combine(storageRoot, request.User.EmployeeId.ToString());
                    var requestFolderPath = Path.Combine(employeeFolder, request.RequestId.ToString());

                    if (!Directory.Exists(requestFolderPath))
                    {
                        Directory.CreateDirectory(requestFolderPath);
                    }

                    // Create a dummy ticket file (if not already created)
                    var ticketFileName = "ticket.txt";
                    var finalFilePath = Path.Combine(requestFolderPath, ticketFileName);

                    if (!System.IO.File.Exists(finalFilePath))
                    {
                        await System.IO.File.WriteAllTextAsync(finalFilePath,
                            $"Dummy ticket for Request {request.RequestId}, Employee {request.User.EmployeeId}\n" +
                            $"Generated at: {DateTime.UtcNow}\n" +
                            $"Postman Provided URL: {actionDto.TicketFileUrl ?? "N/A"}");
                    }

                    // Save relative path in DB
                    var relativePath = $"/travel_docs/{request.User.EmployeeId}/{request.RequestId}/{ticketFileName}";
                    request.TicketFileUrl = relativePath;
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

            // Send email if status changed
            if (oldStatus != request.Status)
            {
                var employeeEmail = request.User?.Email;
                var managerEmail = request.User?.Manager?.Email;

                var subject = $"Travel Request {id} Status Updated";
                var body = $"Travel request (ID: {request.RequestId}) has been updated. " +
                           $"New Status: {request.Status}. Comments: {actionDto.Comments}";

                if (!string.IsNullOrEmpty(employeeEmail))
                {
                    await _emailService.SendEmailAsync(employeeEmail, subject, body);
                }
                if (!string.IsNullOrEmpty(managerEmail))
                {
                    await _emailService.SendEmailAsync(managerEmail, subject, body);
                }
            }

            return Ok(new { Message = $"Request {id} status updated to {request.Status}." });
        }
    }
}
