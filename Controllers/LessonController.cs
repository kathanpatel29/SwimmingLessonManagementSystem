using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using SwimmingLessonManagementSystem.Models;
using System.Web.Script.Serialization;

namespace SwimmingLessonManagementSystem.Controllers
{
    public class LessonController : Controller
    {
        // Static HttpClient instance for making API calls
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        // Static constructor to initialize the HttpClient
        static LessonController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44358/api/lessondata/");
        }

        // GET: Lesson/List
        // Retrieves the list of lessons from the API and displays it
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

        // GET: Lesson/Details/1
        // Retrieves the details of a specific lesson by ID and displays it
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

        // GET: Lesson/Create
        // Displays the form to create a new lesson
        public ActionResult Create()
        {
            // Show create form
            return View();
        }

        // POST: Lesson/Create
        // Handles the form submission to create a new lesson
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

        // GET: Lesson/Edit/1
        // Displays the form to edit an existing lesson
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

        // POST: Lesson/Edit/1
        // Handles the form submission to update an existing lesson
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

        // GET: Lesson/Delete/1
        // Displays the form to confirm deletion of a lesson
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

        // POST: Lesson/Delete/1
        // Handles the form submission to delete a lesson
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
