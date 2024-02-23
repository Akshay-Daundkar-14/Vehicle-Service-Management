using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class ServiceRepresentative
    {
        [Key]
        public int RepresentativeID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(15)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit mobile number.")]
        public string ContactNumber { get; set; } 

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }


        public bool IsDeleted { get; set; } = false;


        [JsonIgnore]
        public ICollection<ServiceRecord>? ServiceRecords { get; set; }
        [JsonIgnore]
        public ICollection<ScheduledService>? ScheduledServices { get; set; }
    }
}
