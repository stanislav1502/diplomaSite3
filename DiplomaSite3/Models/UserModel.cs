﻿
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public enum UserType
    {
        Student, Teacher, Admin
    }
    public abstract class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50,MinimumLength =1)]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        public UserType UserType { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }


    }
}