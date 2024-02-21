using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class ServiceRecordItem
    {
        [Key]
        public int ServiceRecordItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")] 
        public int Quantity { get; set; }

        [ForeignKey("ServiceRecord")]
        public int? ServiceRecordID { get; set; }
        [JsonIgnore]
        public ServiceRecord? ServiceRecord { get; set; }
        [ForeignKey("Material")]
        public int ItemID { get; set; }
        [JsonIgnore]
        public Material? Material { get; set; }
    }
}
