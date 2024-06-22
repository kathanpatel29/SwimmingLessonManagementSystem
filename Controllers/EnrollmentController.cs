using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using SwimmingLessonManagementSystem.Models;

namespace SwimmingLessonManagementSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        private static readonly HttpClient client;

        static EnrollmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44358/api/EnrollmentData/");
        }

        // GET: Enrollment/List
        public ActionResult List(string searchTerm, DateTime? startDate, DateTime? endDate, string progress)
        {
            string url = "listenrollments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<EnrollmentDto> enrollments = response.Content.ReadAsAsync<IEnumerable<EnrollmentDto>>().Result;

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    enrollments = enrollments.Where(e => e.StudentName.Contains(searchTerm) || e.LessonTitle.Contains(searchTerm));
                }

                if (startDate.HasValue)
                {
                    enrollments = enrollments.Where(e => e.EnrollmentDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    enrollments = enrollments.Where(e => e.EnrollmentDate <= endDate.Value);
                }

                if (!string.IsNullOrEmpty(progress))
                {
                    enrollments = enrollments.Where(e => e.Progress.Equals(progress, StringComparison.OrdinalIgnoreCase));
                }

                return View(enrollments);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Enrollment/Details/1
        public async Task<ActionResult> Details(int id)
        {
            string url = $"FindEnrollment/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                EnrollmentDto selectedEnrollment = await response.Content.ReadAsAsync<EnrollmentDto>();
                return View(selectedEnrollment);
            }

            string errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View("Error");
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enrollment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EnrollmentDto enrollment)
        {
            if (!ModelState.IsValid)
            {
                return View(enrollment);
            }

            string url = "AddEnrollment";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, enrollment);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }

            return View(enrollment);
        }

        // GET: Enrollment/Edit/1
        public async Task<ActionResult> Edit(int id)
        {
            string url = $"FindEnrollment/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                EnrollmentDto selectedEnrollment = await response.Content.ReadAsAsync<EnrollmentDto>();
                return View(selectedEnrollment);
            }

            string errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View("Error");
        }

        // POST: Enrollment/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EnrollmentDto enrollment)
        {
            if (!ModelState.IsValid)
            {
                return View(enrollment);
            }

            string url = $"UpdateEnrollment/{id}";
            HttpResponseMessage response = await client.PutAsJsonAsync(url, enrollment);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }

            string errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Failed to update enrollment. Please try again. Error: {errorMessage}");
            return View(enrollment);
        }

        // GET: Enrollment/Delete/1
        public async Task<ActionResult> Delete(int id)
        {
            string url = $"FindEnrollment/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                EnrollmentDto selectedEnrollment = await response.Content.ReadAsAsync<EnrollmentDto>();
                return View(selectedEnrollment);
            }

            string errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View("Error");
        }

        // POST: Enrollment/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string url = $"DeleteEnrollment/{id}";
            HttpResponseMessage response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }

            string errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Failed to delete enrollment. Please try again. Error: {errorMessage}");
            return View("Error");
        }
    }
}
