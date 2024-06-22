using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using SwimmingLessonManagementSystem.Models;

namespace SwimmingLessonManagementSystem.Controllers
{
    public class LessonController : Controller
    {
        private static readonly HttpClient client;

        static LessonController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44358/api/LessonData/");
        }

        // GET: Lesson/List
        public async Task<ActionResult> List()
        {
            string url = "ListLessons";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<LessonDto> lessons = await response.Content.ReadAsAsync<IEnumerable<LessonDto>>();
                return View(lessons);
            }

            // Log the error message for debugging
            string errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View("Error");
        }

        // GET: Lesson/Details/1
        public async Task<ActionResult> Details(int id)
        {
            string url = $"FindLesson/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                LessonDto selectedLesson = await response.Content.ReadAsAsync<LessonDto>();
                return View(selectedLesson);
            }

            // Log the error message for debugging
            string errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View("Error");
        }

        // GET: Lesson/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lesson/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LessonDto lesson)
        {
            if (!ModelState.IsValid)
            {
                return View(lesson);
            }

            string url = "AddLesson";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, lesson);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }

            return View(lesson);
        }

        // GET: Lesson/Edit/1
        public async Task<ActionResult> Edit(int id)
        {
            string url = $"FindLesson/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                LessonDto selectedLesson = await response.Content.ReadAsAsync<LessonDto>();
                return View(selectedLesson);
            }

            // Log the error message for debugging
            string errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View("Error");
        }

        // POST: Lesson/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LessonDto lesson)
        {
            if (!ModelState.IsValid)
            {
                return View(lesson);
            }

            string url = $"UpdateLesson/{id}";
            HttpResponseMessage response = await client.PutAsJsonAsync(url, lesson);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }

            // Log the error message for debugging
            string errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Failed to update lesson. Please try again. Error: {errorMessage}");
            return View(lesson);
        }

        // GET: Lesson/Delete/1
        public async Task<ActionResult> Delete(int id)
        {
            string url = $"FindLesson/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                LessonDto selectedLesson = await response.Content.ReadAsAsync<LessonDto>();
                return View(selectedLesson);
            }

            // Log the error message for debugging
            string errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View("Error");
        }

        // POST: Lesson/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string url = $"DeleteLesson/{id}";
            HttpResponseMessage response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }

            // Log the error message for debugging
            string errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Failed to delete lesson. Please try again. Error: {errorMessage}");
            return View("Error");
        }
    }
}
