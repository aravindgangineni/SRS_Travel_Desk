using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelDesk_Api.Models;

public class TravelRequest
{
    [Key]
    public int RequestId { get; set; }

    [Required]
    public string EmployeeId { get; set; }

    [Required]
    public string EmployeeName { get; set; }

    [Required]
    public string ProjectName { get; set; }

    [Required]
    public string DepartmentName { get; set; }

    [Required]
    public string ReasonForTravelling { get; set; }

    [Required]
    public string TypeOfBooking { get; set; }

    public string? FlightType { get; set; }

    public DateTime? CheckinDate { get; set; }
    public DateTime? FlightDate { get; set; }

    public string? AadhaarNumber { get; set; }

    public string? PassportNumber { get; set; }

    public string? VisaFileUrl { get; set; }

    public string? PassportFileUrl { get; set; }

    public string? TicketFileUrl { get; set; }

    public int? DaysOfStay { get; set; }

    public string? MealRequired { get; set; }

    public string? MealPreference { get; set; }

    [Required]
    public string Status { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    public ICollection<RequestComment> Comments { get; set; }

    public int? ManagerId { get; set; }

    [ForeignKey("ManagerId")]
    public User? Manager { get; set; }

    public string? ManagerName { get; set; }

    public bool IsDeleted { get; set; } = false;
}