using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SwimmingLessonManagementSystem.Models;

namespace SwimmingLessonManagementSystem.Controllers
{
    public class EnrollmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EnrollmentData/ListEnrollments
        // Retrieves a list of enrollments along with associated lesson and student information
        [HttpGet]
        [Route("api/EnrollmentData/ListEnrollments")]
        public IEnumerable<EnrollmentDto> ListEnrollments()
        {
            List<Enrollment> enrollments = db.Enrollments.Include(e => e.Lesson).Include(e => e.Student).ToList();
            List<EnrollmentDto> enrollmentDtos = new List<EnrollmentDto>();

            // Map Enrollment objects to EnrollmentDto objects
            enrollments.ForEach(e => enrollmentDtos.Add(new EnrollmentDto()
            {
                EnrollmentID = e.EnrollmentID,
                LessonID = e.LessonID,
                LessonTitle = e.Lesson.Title,
                StudentID = e.StudentID,
                StudentName = e.Student.Username,
                Progress = e.Progress,
            }));

            return enrollmentDtos;
        }

        // GET: api/EnrollmentData/FindEnrollment/5
        // Retrieves details of a specific enrollment by ID, including associated lesson and student information
        [ResponseType(typeof(EnrollmentDto))]
        [HttpGet]
        [Route("api/EnrollmentData/FindEnrollment/{id}")]
        public IHttpActionResult FindEnrollment(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            // Map Enrollment object to EnrollmentDto object
            EnrollmentDto enrollmentDto = new EnrollmentDto()
            {
                EnrollmentID = enrollment.EnrollmentID,
                LessonID = enrollment.LessonID,
                LessonTitle = enrollment.Lesson != null ? enrollment.Lesson.Title : "N/A",
                StudentID = enrollment.StudentID,
                StudentName = enrollment.Student != null ? enrollment.Student.Username : "N/A",
                Progress = enrollment.Progress
            };

            return Ok(enrollmentDto);
        }

        // POST: api/EnrollmentData/AddEnrollment
        // Adds a new enrollment record
        [ResponseType(typeof(EnrollmentDto))]
        [HttpPost]
        [Route("api/EnrollmentData/AddEnrollment")]
        public IHttpActionResult AddEnrollment(EnrollmentDto enrollmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map EnrollmentDto object to Enrollment object
            Enrollment enrollment = new Enrollment()
            {
                LessonID = enrollmentDto.LessonID,
                StudentID = enrollmentDto.StudentID,
                Progress = enrollmentDto.Progress
            };

            // Save the new enrollment record to the database
            db.Enrollments.Add(enrollment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = enrollment.EnrollmentID }, enrollmentDto);
        }

        // PUT: api/EnrollmentData/UpdateEnrollment/id
        // Updates an existing enrollment record
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/EnrollmentData/UpdateEnrollment/{id}")]
        public IHttpActionResult UpdateEnrollment(int id, EnrollmentDto enrollmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrollmentDto.EnrollmentID)
            {
                return BadRequest();
            }

            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            // Update the enrollment record
            enrollment.LessonID = enrollmentDto.LessonID;
            enrollment.StudentID = enrollmentDto.StudentID;
            enrollment.Progress = enrollmentDto.Progress;

            // Save the changes to the database
            db.Entry(enrollment).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/EnrollmentData/DeleteEnrollment/id
        // Deletes an existing enrollment record
        [ResponseType(typeof(Enrollment))]
        [HttpDelete]
        [Route("api/EnrollmentData/DeleteEnrollment/{id}")]
        public IHttpActionResult DeleteEnrollment(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            // Remove the enrollment record from the database
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();

            return Ok(enrollment);
        }
    }
}
