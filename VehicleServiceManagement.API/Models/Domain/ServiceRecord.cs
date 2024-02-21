using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class ServiceRecord
    {
        [Key]
        public int ServiceRecordID { get; set; }
        public int RepresentativeID { get; set; }
        public ServiceRepresentative Representative { get; set; }

        [Required]
        public DateTime ServiceDate { get; set; }
        [JsonIgnore]
        public ICollection<ServiceRecordItem>? ServiceRecordItems { get; set; }
         
        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }
        [JsonIgnore]
        public Vehicle? Vehicle { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}
