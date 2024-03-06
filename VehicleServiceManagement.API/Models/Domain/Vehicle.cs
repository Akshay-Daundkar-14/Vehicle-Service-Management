using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required]
        [StringLength(255)]
        public string VehicleCategory { get; set; }

        [Required]
        [StringLength(255)]
        public string VehicleRegNo { get; set; } 

        [Required]
        [StringLength(255)]
        public string VehicleNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string VehicleModel { get; set; }

        [Required]
        [StringLength(255)]
        public string VehicleBrand { get; set; }

       
        [StringLength(255)]
        public string? VehicleStatus { get; set; }

        [StringLength(255)]
        public string? Image { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [JsonIgnore]
        public ICollection<ServiceRecord>? ServiceRecords { get; set; }

        [JsonIgnore]
        public ICollection<ScheduledService>? ScheduledServices { get; set; }
    }
}
