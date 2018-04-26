using System;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}