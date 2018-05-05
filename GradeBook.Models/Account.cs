using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(20)]
        public string FirstName { get; set; }
        
        [Required, MaxLength(20)]
        public string LastName { get; set; }
        
        [MaxLength(20)]
        public string MiddleName { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        [Required, MaxLength(30)]
        public string Login { get; set; }
        
        [Required]
        public string PasswordSalt { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        [Required, MaxLength(20)]
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}