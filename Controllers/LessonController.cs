using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using SwimmingLessonManagementSystem.Models;
using System.Net;

namespace SwimmingLessonManagementSystem.Controllers
{
    /// <summary>
    /// Controller for managing lesson-related views and interactions.
    /// </summary>
    public class LessonController : Controller
    {
        // Static HttpClient instance for making API calls
        private static readonly HttpClient client;

        // Static constructor to initialize the HttpClient
        static LessonController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44358/api/lessondata/");
        }

        /// <summary>
        /// Retrieves the list of lessons from the API and displays it.
        /// </summary>
        /// <returns>The view containing the list of lessons.</returns>
        /// <example>
        /// GET: Lesson/List
        /// </example>
        public ActionResult List()
        {
            string url = "listlessons";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<LessonDto> lessons = response.Content.ReadAsAsync<IEnumerable<LessonDto>>().Result;
                return View(lessons);
            }
            else
            {
                // Handle error (display error message or retry)
                return View("Error");
            }
        }

        /// <summary>
        /// Retrieves the details of a specific lesson by ID and displays it.
        /// </summary>
        /// <param name="id">The ID of the lesson to retrieve.</param>
        /// <returns>The view containing the details of the selected lesson.</returns>
        /// <example>
        /// GET: Lesson/Details/1
        /// </example>
        public ActionResult Details(int id)
        {
            string url = "findlesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                LessonDto selectedLesson = response.Content.ReadAsAsync<LessonDto>().Result;
                return View(selectedLesson);
            }
            else
            {
                // Handle error (display error message or retry)
                return View("Error");
            }
        }

        /// <summary>
        /// Displays the form to create a new lesson.
        /// </summary>
        /// <returns>The view for creating a new lesson.</returns>
        /// <example>
        /// GET: Lesson/Create
        /// </example>
        public ActionResult Create()
        {
            // Show create form
            return View();
        }

        /// <summary>
        /// Handles the form submission to create a new lesson.
        /// </summary>
        /// <param name="lesson">The LessonDto object containing the lesson data.</param>
        /// <returns>Redirects to the list of lessons after successful creation, or displays error view on failure.</returns>
        /// <example>
        /// POST: Lesson/Create
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LessonDto lesson)
        {
            string url = "addlesson";
            HttpResponseMessage response = client.PostAsJsonAsync(url, lesson).Result;
            if (response.IsSuccessStatusCode)
            {
                // Redirect to list after successful creation
                return RedirectToAction("List");
            }
            else
            {
                // Handle error (display error message or retry)
                return View(lesson);
            }
        }

        /// <summary>
        /// Displays the form to edit an existing lesson.
        /// </summary>
        /// <param name="id">The ID of the lesson to edit.</param>
        /// <returns>The view for editing an existing lesson.</returns>
        /// <example>
        /// GET: Lesson/Edit/1
        /// </example>
        public ActionResult Edit(int id)
        {
            string url = "findlesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                LessonDto selectedLesson = response.Content.ReadAsAsync<LessonDto>().Result;
                return View(selectedLesson);
            }
            else
            {
                // Handle error (display error message or retry)
                return View("Error");
            }
        }

        /// <summary>
        /// Handles the form submission to update an existing lesson.
        /// </summary>
        /// <param name="id">The ID of the lesson to update.</param>
        /// <param name="lesson">The LessonDto object containing the updated lesson data.</param>
        /// <returns>Redirects to the details of the updated lesson after successful update, or displays error view on failure.</returns>
        /// <example>
        /// POST: Lesson/Edit/1
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LessonDto lesson)
        {
            string url = "updatelesson/" + id;
            HttpResponseMessage response = client.PutAsJsonAsync(url, lesson).Result;
            if (response.IsSuccessStatusCode)
            {
                // Redirect to details after successful update
                return RedirectToAction("Details", new { id });
            }
            else
            {
                // Handle error (display error message or retry)
                return View(lesson);
            }
        }

        /// <summary>
        /// Displays the form to confirm deletion of a lesson.
        /// </summary>
        /// <param name="id">The ID of the lesson to delete.</param>
        /// <returns>The view for confirming deletion of a lesson.</returns>
        /// <example>
        /// GET: Lesson/Delete/1
        /// </example>
        public ActionResult Delete(int id)
        {
            string url = "findlesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                LessonDto selectedLesson = response.Content.ReadAsAsync<LessonDto>().Result;
                return View(selectedLesson);
            }
            else
            {
                // Handle error (display error message or retry)
                return View("Error");
            }
        }

        /// <summary>
        /// Handles the form submission to delete a lesson.
        /// </summary>
        /// <param name="id">The ID of the lesson to delete.</param>
        /// <param name="collection">The form collection.</param>
        /// <returns>Redirects to the list of lessons after successful deletion, or displays error view on failure.</returns>
        /// <example>
        /// POST: Lesson/Delete/1
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "deletelesson/" + id;
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                // Redirect to list after successful deletion
                return RedirectToAction("List");
            }
            else
            {
                // Handle error (display error message or retry)
                return View("Error");
            }
        }
    }
}
