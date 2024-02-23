using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class ScheduledService
    {
        [Key]
        public int ScheduledServiceId { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        public DateTime ScheduledDate { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }
        [JsonIgnore]
        public Vehicle? Vehicle { get; set; }
        [ForeignKey("ServiceRepresentative")]
        public int ServiceAdvisorID { get; set; }
        [JsonIgnore]
        public ServiceRepresentative? ServiceRepresentative { get; set; } 
    }
}
