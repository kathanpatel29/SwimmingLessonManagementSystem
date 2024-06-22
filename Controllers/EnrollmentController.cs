using System.Linq;
using System.Web.Mvc;
using SwimmingLessonManagementSystem.Models;

namespace SwimmingLessonManagementSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enrollment
        public ActionResult List()
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
            }).ToList();

            return View(enrollments);
        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int id)
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
                return HttpNotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.LessonID = new SelectList(db.Lessons, "LessonID", "Title");
            ViewBag.StudentID = new SelectList(db.Users.Where(u => u.Role == "Student"), "UserID", "Username");
            return View();
        }

        // POST: Enrollment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,EnrollmentDate,LessonID,StudentID,Progress")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LessonID = new SelectList(db.Lessons, "LessonID", "Title", enrollment.LessonID);
            ViewBag.StudentID = new SelectList(db.Users.Where(u => u.Role == "Student"), "UserID", "Username", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }

            ViewBag.LessonID = new SelectList(db.Lessons, "LessonID", "Title", enrollment.LessonID);
            ViewBag.StudentID = new SelectList(db.Users.Where(u => u.Role == "Student"), "UserID", "Username", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,EnrollmentDate,LessonID,StudentID,Progress")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LessonID = new SelectList(db.Lessons, "LessonID", "Title", enrollment.LessonID);
            ViewBag.StudentID = new SelectList(db.Users.Where(u => u.Role == "Student"), "UserID", "Username", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public ActionResult Delete(int id)
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
                return HttpNotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
