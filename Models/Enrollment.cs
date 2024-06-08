// Necessary namespaces for data annotations and ORM
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimmingLessonManagementSystem.Models
{
    // This class represents an Enrollment in a lesson
    public class Enrollment
    {
        // Primary key for the Enrollment entity
        [Key]
        public int EnrollmentID { get; set; }

        // Date when the enrollment was made
        public DateTime EnrollmentDate { get; set; }

        // Foreign key referencing the Lesson entity
        [ForeignKey("Lesson")]
        public int LessonID { get; set; }

        // Navigation property for the Lesson entity
        public Lesson Lesson { get; set; }

        // Foreign key referencing the Student (User) entity
        public int StudentID { get; set; }

        // Navigation property for the Student (User) entity
        public User Student { get; set; }

        // Progress status of the student in the lesson
        // Possible values: Not Started, In Progress, Completed
        public string Progress { get; set; }

        // Reference to the User entity, though this seems redundant given StudentID and Student
        public User UserID { get; set; }
    }

    // Data Transfer Object for Enrollment
    // Used to transfer enrollment data without exposing the entire Enrollment model
    public class EnrollmentDto
    {
        // Properties match the Enrollment model but used in a different context (e.g., API responses)
        public int EnrollmentID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int LessonID { get; set; }
        public string LessonTitle { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }

        // Progress status of the student in the lesson
        // Possible values: Not Started, In Progress, Completed
        public string Progress { get; set; }

        // Reference to the User entity, though this seems redundant given StudentID and StudentName
        public User UserID { get; set; }
    }
}
