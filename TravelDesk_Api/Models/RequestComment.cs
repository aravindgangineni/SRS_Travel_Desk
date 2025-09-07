using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TravelDesk_Api.Models;

public class RequestComment
{
    [Key]
    public int CommentId { get; set; }

    [Required]
    public string Comment { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public int RequestId { get; set; }

    [ForeignKey("RequestId")]
    [JsonIgnore]
    public TravelRequest TravelRequest { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }

    public bool IsDeleted { get; set; } = false;
}