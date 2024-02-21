using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class Material
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemName { get; set; }

        [Required]
        [Range(0,double.MaxValue, ErrorMessage = "Cost must be greater than 0.")]
        public double Cost { get; set; }


        [JsonIgnore]
        public ICollection<ServiceRecordItem>? ServiceRecordItems { get; set; } 
    }
}
