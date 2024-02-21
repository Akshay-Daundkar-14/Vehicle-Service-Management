using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(100)]
        public string Bank { get; set; }

        [StringLength(100)]
        public string Branch { get; set; } 

        [StringLength(20)]
        public string CardNo { get; set; }

        [StringLength(10)]
        public string CVV { get; set; }

        [Required]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}
