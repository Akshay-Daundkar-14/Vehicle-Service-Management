using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; } 

        [Required]
        [StringLength(20)]
        [RegularExpression("^(Admin|ServiceAdvisor)$", ErrorMessage = "UserType must be either 'Admin' or 'ServiceAdvisor'.")]
        public string UserType { get; set; }
    }
}
