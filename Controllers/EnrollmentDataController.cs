using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using SwimmingLessonManagementSystem.Models;
using System.Data.Entity.Infrastructure;

namespace SwimmingLessonManagementSystem.Controllers
{
    public class EnrollmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EnrollmentData/ListEnrollments
        [HttpGet]
        [Route("api/EnrollmentData/ListEnrollments")]
        public IQueryable<EnrollmentDto> ListEnrollments()
        {
            var enrollments = db.Enrollments.Select(e => new EnrollmentDto
            {
                EnrollmentID = e.EnrollmentID,
                EnrollmentDate = e.EnrollmentDate,
                LessonID = e.LessonID,
                LessonTitle = e.Lesson.Title,
                StudentID = e.StudentID,
                StudentName = e.Student.Username,
                Progress = e.Progress
            });

            return enrollments;
        }

        // GET: api/EnrollmentData/FindEnrollment/5
        [HttpGet]
        [Route("api/EnrollmentData/FindEnrollment/{id}")]
        [ResponseType(typeof(EnrollmentDto))]
        public IHttpActionResult FindEnrollment(int id)
        {
            var enrollment = db.Enrollments.Where(e => e.EnrollmentID == id)
                .Select(e => new EnrollmentDto
                {
                    EnrollmentID = e.EnrollmentID,
                    EnrollmentDate = e.EnrollmentDate,
                    LessonID = e.LessonID,
                    LessonTitle = e.Lesson.Title,
                    StudentID = e.StudentID,
                    StudentName = e.Student.Username,
                    Progress = e.Progress
                }).FirstOrDefault();

            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(enrollment);
        }

        // POST: api/EnrollmentData/UpdateEnrollment/5
        [HttpPost]
        [Route("api/EnrollmentData/UpdateEnrollment/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateEnrollment(int id, Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrollment.EnrollmentID)
            {
                return BadRequest();
            }

            db.Entry(enrollment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EnrollmentData/AddEnrollment
        [HttpPost]
        [Route("api/EnrollmentData/AddEnrollment")]
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult AddEnrollment(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Enrollments.Add(enrollment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = enrollment.EnrollmentID }, enrollment);
        }

        // POST: api/EnrollmentData/DeleteEnrollment/5
        [HttpPost]
        [Route("api/EnrollmentData/DeleteEnrollment/{id}")]
        [ResponseType(typeof(Enrollment))]
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

        private bool EnrollmentExists(int id)
        {
            return db.Enrollments.Count(e => e.EnrollmentID == id) > 0;
        }
    }
}
