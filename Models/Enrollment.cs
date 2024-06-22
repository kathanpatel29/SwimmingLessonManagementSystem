using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimmingLessonManagementSystem.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [ForeignKey("Lesson")]
        public int LessonID { get; set; }
        public Lesson Lesson { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public User Student { get; set; }

        public string Progress { get; set; }
    }

    public class EnrollmentDto
    {
        public int EnrollmentID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int LessonID { get; set; }
        public string LessonTitle { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Progress { get; set; }
    }
}
