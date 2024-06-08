// Necessary namespaces for data annotations and ORM
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimmingLessonManagementSystem.Models
{
    // This class represents a User in the system
    public class User
    {
        // Primary key for the User entity
        [Key]
        public int UserID { get; set; }

        // Username is required and should be unique for each user
        [Required]
        public string Username { get; set; }

        // Email is required for user identification and communication
        [Required]
        public string Email { get; set; }

        // Role indicates whether the user is an Instructor or a Student
        [Required]
        public string Role { get; set; }
    }

    // Data Transfer Object for User
    // Used to transfer user data without exposing the entire User model
    public class UserDto
    {
        // Properties match the User model but used in a different context (e.g., API responses)
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
