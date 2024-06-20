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
    /// API Controller for managing lessons in the Swimming Lesson Management System.
    /// </summary>
    public class LessonDataController : ApiController
    {
        // Database context for accessing the database
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all lessons and returns it as a collection of LessonDto objects.
        /// </summary>
        /// <returns>A collection of LessonDto objects.</returns>
        /// <example>
        /// GET: api/LessonData/ListLessons
        /// </example>
        [HttpGet]
        [Route("api/LessonData/ListLessons")]
        public IEnumerable<LessonDto> ListLessons()
        {
            List<Lesson> lessons = db.Lessons.ToList();
            List<LessonDto> lessonDtos = new List<LessonDto>();

            lessons.ForEach(l => lessonDtos.Add(new LessonDto()
            {
                LessonID = l.LessonID,
                Title = l.Title,
                Date = l.Date.Date,
                Time = l.Time,
                Description = l.Description,
                InstructorID = l.InstructorID,
                InstructorName = l.Instructor.Username,
                Capacity = l.Capacity
            }));

            return lessonDtos;
        }

        /// <summary>
        /// Retrieves the details of a specific lesson by ID and returns it as a LessonDto object.
        /// </summary>
        /// <param name="id">The ID of the lesson to retrieve.</param>
        /// <returns>An IHttpActionResult containing the LessonDto object.</returns>
        /// <example>
        /// GET: api/LessonData/FindLesson/5
        /// </example>
        [ResponseType(typeof(LessonDto))]
        [HttpGet]
        [Route("api/LessonData/FindLesson/{id}")]
        public IHttpActionResult FindLesson(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            LessonDto lessonDto = new LessonDto()
            {
                LessonID = lesson.LessonID,
                Title = lesson.Title,
                Date = lesson.Date.Date,
                Time = lesson.Time,
                Description = lesson.Description,
                InstructorID = lesson.InstructorID,
                InstructorName = lesson.Instructor.Username,
                Capacity = lesson.Capacity
            };

            return Ok(lessonDto);
        }

        /// <summary>
        /// Adds a new lesson to the system.
        /// </summary>
        /// <param name="lessonDto">The LessonDto object containing the lesson data.</param>
        /// <returns>An IHttpActionResult containing the created LessonDto object.</returns>
        /// <example>
        /// POST: api/LessonData/AddLesson
        /// </example>
        [ResponseType(typeof(LessonDto))]
        [HttpPost]
        [Route("api/LessonData/AddLesson")]
        public IHttpActionResult AddLesson(LessonDto lessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lesson lesson = new Lesson()
            {
                Title = lessonDto.Title,
                Date = lessonDto.Date.Date,
                Time = lessonDto.Time,
                InstructorID = lessonDto.InstructorID,
                Capacity = lessonDto.Capacity
            };

            db.Lessons.Add(lesson);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lesson.LessonID }, lessonDto);
        }

        /// <summary>
        /// Updates an existing lesson's details.
        /// </summary>
        /// <param name="id">The ID of the lesson to update.</param>
        /// <param name="lessonDto">The LessonDto object containing the updated lesson data.</param>
        /// <returns>An IHttpActionResult with the status of the update operation.</returns>
        /// <example>
        /// PUT: api/LessonData/UpdateLesson/id
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/LessonData/UpdateLesson/{id}")]
        public IHttpActionResult UpdateLesson(int id, LessonDto lessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lessonDto.LessonID)
            {
                return BadRequest();
            }

            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            lesson.Title = lessonDto.Title;
            lesson.Date = lessonDto.Date.Date;
            lesson.Time = lessonDto.Time;
            lesson.Description = lessonDto.Description;
            lesson.InstructorID = lessonDto.InstructorID;
            lesson.Capacity = lessonDto.Capacity;

            db.Entry(lesson).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes a lesson from the system.
        /// </summary>
        /// <param name="id">The ID of the lesson to delete.</param>
        /// <returns>An IHttpActionResult containing the deleted Lesson object.</returns>
        /// <example>
        /// DELETE: api/LessonData/DeleteLesson/id
        /// </example>
        [ResponseType(typeof(Lesson))]
        [HttpDelete]
        [Route("api/LessonData/DeleteLesson/{id}")]
        public IHttpActionResult DeleteLesson(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            db.Lessons.Remove(lesson);
            db.SaveChanges();

            return Ok(lesson);
        }
    }
}
