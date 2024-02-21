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
        public string ContactNumber { get; set; } 

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [JsonIgnore]
        public ICollection<ServiceRecord>? ServiceRecords { get; set; }
        [JsonIgnore]
        public ICollection<ScheduledService>? ScheduledServices { get; set; }
    }
}
