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
    public class EnrollmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EnrollmentData/ListEnrollments
        [HttpGet]
        [Route("api/EnrollmentData/ListEnrollments")]
        public IEnumerable<EnrollmentDto> ListEnrollments()
        {
            List<Enrollment> enrollments = db.Enrollments.Include(e => e.Lesson).Include(e => e.Student).ToList();
            List<EnrollmentDto> enrollmentDtos = new List<EnrollmentDto>();

            enrollments.ForEach(e => enrollmentDtos.Add(new EnrollmentDto()
            {
                EnrollmentID = e.EnrollmentID,
                EnrollmentDate = e.EnrollmentDate,
                LessonID = e.LessonID,
                LessonTitle = e.Lesson.Title,
                StudentID = e.StudentID,
                StudentName = e.Student.Username,
                Progress = e.Progress,
            }));

            return enrollmentDtos;
        }

        // GET: api/EnrollmentData/FindEnrollment/5
        [ResponseType(typeof(EnrollmentDto))]
        [HttpGet]
        [Route("api/EnrollmentData/FindEnrollment/{id}")]
        public IHttpActionResult FindEnrollment(int id)
        {
            Enrollment enrollment = db.Enrollments.Include(e => e.Lesson).Include(e => e.Student).FirstOrDefault(e => e.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            EnrollmentDto enrollmentDto = new EnrollmentDto()
            {
                EnrollmentID = enrollment.EnrollmentID,
                EnrollmentDate = enrollment.EnrollmentDate,
                LessonID = enrollment.LessonID,
                LessonTitle = enrollment.Lesson.Title,
                StudentID = enrollment.StudentID,
                StudentName = enrollment.Student.Username,
                Progress = enrollment.Progress,
            };

            return Ok(enrollmentDto);
        }

        // POST: api/EnrollmentData/AddEnrollment
        [ResponseType(typeof(EnrollmentDto))]
        [HttpPost]
        [Route("api/EnrollmentData/AddEnrollment")]
        public IHttpActionResult AddEnrollment(EnrollmentDto enrollmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Enrollment enrollment = new Enrollment()
            {
                EnrollmentDate = enrollmentDto.EnrollmentDate,
                LessonID = enrollmentDto.LessonID,
                StudentID = enrollmentDto.StudentID,
                Progress = enrollmentDto.Progress,
            };

            db.Enrollments.Add(enrollment);
            db.SaveChanges();

            enrollmentDto.EnrollmentID = enrollment.EnrollmentID;

            return CreatedAtRoute("DefaultApi", new { id = enrollment.EnrollmentID }, enrollmentDto);
        }

        // PUT: api/EnrollmentData/UpdateEnrollment/5
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

            enrollment.EnrollmentDate = enrollmentDto.EnrollmentDate;
            enrollment.LessonID = enrollmentDto.LessonID;
            enrollment.StudentID = enrollmentDto.StudentID;

            db.Entry(enrollment).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/EnrollmentData/DeleteEnrollment/5
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

            db.Enrollments.Remove(enrollment);
            db.SaveChanges();

            return Ok(enrollment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
