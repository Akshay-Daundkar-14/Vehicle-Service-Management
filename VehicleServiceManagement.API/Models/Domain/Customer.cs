﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VehicleServiceManagement.API.Models.Domain
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
         
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Mobile number must be between 10 to 15 digits.")]
        public string Mobile { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [JsonIgnore]
        public ICollection<ServiceRecord>? ServiceRecords { get; set; }
        [JsonIgnore]
        public ICollection<Payment>? Payments { get; set; }
        [JsonIgnore]
        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
