using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SwimmingLessonManagementSystem.Models;

namespace SwimmingLessonManagementSystem.Controllers
{
    /// <summary>
    /// API Controller for managing enrollments, including CRUD operations.
    /// </summary>
    public class EnrollmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of enrollments along with associated lesson and student information.
        /// </summary>
        /// <returns>An IEnumerable of EnrollmentDto objects representing the list of enrollments.</returns>
        /// <example>
        /// GET: api/EnrollmentData/ListEnrollments
        /// </example>
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

        /// <summary>
        /// Retrieves details of a specific enrollment by ID, including associated lesson and student information.
        /// </summary>
        /// <param name="id">The ID of the enrollment to retrieve.</param>
        /// <returns>The IHttpActionResult containing the EnrollmentDto object if found, otherwise NotFound.</returns>
        /// <example>
        /// GET: api/EnrollmentData/FindEnrollment/5
        /// </example>
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

        /// <summary>
        /// Adds a new enrollment record to the database.
        /// </summary>
        /// <param name="enrollmentDto">The EnrollmentDto object containing enrollment data to add.</param>
        /// <returns>The IHttpActionResult containing the newly created EnrollmentDto object, or BadRequest if ModelState is invalid.</returns>
        /// <example>
        /// POST: api/EnrollmentData/AddEnrollment
        /// </example>
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

        /// <summary>
        /// Updates an existing enrollment record in the database.
        /// </summary>
        /// <param name="id">The ID of the enrollment to update.</param>
        /// <param name="enrollmentDto">The EnrollmentDto object containing updated enrollment data.</param>
        /// <returns>StatusCode.NoContent if update is successful, BadRequest if ModelState is invalid, NotFound if enrollment not found.</returns>
        /// <example>
        /// PUT: api/EnrollmentData/UpdateEnrollment/5
        /// </example>
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

        /// <summary>
        /// Deletes an existing enrollment record from the database.
        /// </summary>
        /// <param name="id">The ID of the enrollment to delete.</param>
        /// <returns>Ok containing the deleted Enrollment object, or NotFound if enrollment not found.</returns>
        /// <example>
        /// DELETE: api/EnrollmentData/DeleteEnrollment/5
        /// </example>
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
