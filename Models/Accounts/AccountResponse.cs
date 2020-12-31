using System;

namespace WebApi.Models.Accounts
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FatherFullName { get; set; }
        public string MotherFullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsVerified { get; set; }

        public string ProfilePhoto { get; set; }
    }
}