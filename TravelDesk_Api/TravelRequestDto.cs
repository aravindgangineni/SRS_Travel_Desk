using System.ComponentModel.DataAnnotations;

public class TravelRequestDto
{
    [Required]
    public string EmployeeId { get; set; }

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

    public int? DaysOfStay { get; set; }

    public string? MealRequired { get; set; }

    public string? MealPreference { get; set; }
}