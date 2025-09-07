using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TravelDesk_Api.Models;
using System.Security.Claims;
using System.Linq;
using System.IO;

namespace TravelDesk_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly TravelDeskContext _context;
        private readonly EmailService _emailService;

        public EmployeeController(TravelDeskContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("create-request")]
        public async Task<IActionResult> CreateRequest([FromBody] TravelRequestDto requestDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized("User ID not found in token.");
            int userId = int.Parse(userIdClaim.Value);

            var user = await _context.Users
                                     .Include(u => u.Manager)
                                     .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return NotFound("User not found.");

            var travelRequest = new TravelRequest
            {
                UserId = userId,
                EmployeeId = user.EmployeeId,
                EmployeeName = user.FirstName + " " + user.LastName,
                ProjectName = requestDto.ProjectName,
                DepartmentName = requestDto.DepartmentName,
                ReasonForTravelling = requestDto.ReasonForTravelling,
                TypeOfBooking = requestDto.TypeOfBooking,
                FlightType = requestDto.FlightType,
                CheckinDate = requestDto.CheckinDate,
                FlightDate = requestDto.FlightDate,
                AadhaarNumber = requestDto.AadhaarNumber,
                PassportNumber = requestDto.PassportNumber,
                VisaFileUrl = requestDto.VisaFileUrl,
                PassportFileUrl = requestDto.PassportFileUrl,
                DaysOfStay = requestDto.DaysOfStay,
                MealRequired = requestDto.MealRequired,
                MealPreference = requestDto.MealPreference,
                Status = "Pending",
                ManagerId = user.ManagerId,
                ManagerName = user.Manager != null ? user.Manager.FirstName + " " + user.Manager.LastName : "N/A"
            };

            _context.TravelRequests.Add(travelRequest);
            await _context.SaveChangesAsync();

            // Create a unique folder for the request
            var requestFolderPath = Path.Combine("C:\\travel_desk_docs", travelRequest.RequestId.ToString());
            Directory.CreateDirectory(requestFolderPath);

            // Send email to the manager
            if (user.Manager != null && !string.IsNullOrEmpty(user.Manager.Email))
            {
                var subject = $"New Travel Request from {user.FirstName} {user.LastName}";
                var body = $"A new travel request (ID: {travelRequest.RequestId}) has been submitted for your approval.";
                await _emailService.SendEmailAsync(user.Manager.Email, subject, body);
            }

            return Ok(new { Message = "Request submitted successfully.", RequestId = travelRequest.RequestId });
        }

        [HttpGet("my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized("User ID not found in token.");
            int userId = int.Parse(userIdClaim.Value);

            var requests = await _context.TravelRequests
                                         .Include(tr => tr.Comments)
                                         .Where(tr => tr.UserId == userId)
                                         .OrderByDescending(tr => tr.RequestId)
                                         .ToListAsync();

            return Ok(requests);
        }

        [HttpPut("edit-request/{id}")]
        public async Task<IActionResult> EditRequest(int id, [FromBody] TravelRequestDto updatedRequest)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized("User ID not found in token.");
            int userId = int.Parse(userIdClaim.Value);

            var requestToEdit = await _context.TravelRequests.FindAsync(id);

            if (requestToEdit == null || requestToEdit.UserId != userId)
            {
                return NotFound("Request not found or you do not have permission to edit it.");
            }

            if (requestToEdit.Status != "Returned to Employee")
            {
                return BadRequest("This request cannot be edited in its current status.");
            }

            requestToEdit.ProjectName = updatedRequest.ProjectName;
            requestToEdit.DepartmentName = updatedRequest.DepartmentName;
            requestToEdit.ReasonForTravelling = updatedRequest.ReasonForTravelling;
            requestToEdit.TypeOfBooking = updatedRequest.TypeOfBooking;
            requestToEdit.FlightType = updatedRequest.FlightType;
            requestToEdit.CheckinDate = updatedRequest.CheckinDate;
            requestToEdit.FlightDate = updatedRequest.FlightDate;
            requestToEdit.AadhaarNumber = updatedRequest.AadhaarNumber;
            requestToEdit.PassportNumber = updatedRequest.PassportNumber;
            requestToEdit.VisaFileUrl = updatedRequest.VisaFileUrl;
            requestToEdit.PassportFileUrl = updatedRequest.PassportFileUrl;
            requestToEdit.DaysOfStay = updatedRequest.DaysOfStay;
            requestToEdit.MealRequired = updatedRequest.MealRequired;
            requestToEdit.MealPreference = updatedRequest.MealPreference;
            requestToEdit.Status = "Pending";

            await _context.SaveChangesAsync();

            return Ok(requestToEdit);
        }

        [HttpDelete("delete-request/{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized("User ID not found in token.");
            int userId = int.Parse(userIdClaim.Value);

            var requestToDelete = await _context.TravelRequests.FindAsync(id);

            if (requestToDelete == null || requestToDelete.UserId != userId)
            {
                return NotFound("Request not found or you do not have permission to delete it.");
            }

            requestToDelete.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}