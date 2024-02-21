using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Created_Date { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; } 

    }
}
