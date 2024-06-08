// Necessary namespaces for data annotations and ORM
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimmingLessonManagementSystem.Models
{
    // This class represents a Lesson in the system
    public class Lesson
    {
        // Primary key for the Lesson entity
        [Key]
        public int LessonID { get; set; }

        // Title of the lesson
        public string Title { get; set; } // Lesson title

        // Date when the lesson takes place
        public DateTime Date { get; set; } // Lesson date

        // Time when the lesson starts
        public TimeSpan Time { get; set; } // Lesson time

        // Description of the lesson content
        public string Description { get; set; }

        // Foreign key referencing the Instructor who teaches this lesson
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }

        // Navigation property for the Instructor entity
        public virtual User Instructor { get; set; }

        // Maximum number of students that can enroll in this lesson
        public int Capacity { get; set; } // Maximum capacity for the lesson

        // Collection of enrollments, representing the students enrolled in this lesson
        public ICollection<Enrollment> Enrollments { get; set; }
    }

    // Data Transfer Object for Lesson
    // Used to transfer lesson data without exposing the entire Lesson model
    public class LessonDto
    {
        // Properties match the Lesson model but used in a different context (e.g., API responses)
        public int LessonID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Description { get; set; }

        // Instructor information included in the DTO
        public int InstructorID { get; set; }
        public string InstructorName { get; set; }

        // Maximum capacity for the lesson
        public int Capacity { get; set; }
    }
}
