using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Accounts
{
    public class RegisterRequest
    {
       
        [Required]
        public string MotherFullName { get; set; }

        [Required]
        public string FatherFullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Range(typeof(bool), "true", "true")]
        public bool AcceptTerms { get; set; }

        [Required]
        public int CountryCode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string MobileNumber { get; set; }


    }
}