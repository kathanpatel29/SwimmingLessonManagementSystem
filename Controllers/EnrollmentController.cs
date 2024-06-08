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
    public class EnrollmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static EnrollmentController()
        {
            // Set up HttpClient with base address
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44358/api/enrollmentdata/");
        }

        // GET: Enrollment/List
        // Displays a list of enrollments
        public ActionResult List()
        {
            string url = "listenrollments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Deserialize response content to a list of EnrollmentDto objects
            IEnumerable<EnrollmentDto> enrollments = response.Content.ReadAsAsync<IEnumerable<EnrollmentDto>>().Result;

            // Render the view with the list of enrollments
            return View(enrollments);
        }

        // GET: Enrollment/Details/1
        // Displays details of a specific enrollment
        public ActionResult Details(int id)
        {
            string url = "findenrollment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Deserialize response content to an EnrollmentDto object
            EnrollmentDto selectedEnrollment = response.Content.ReadAsAsync<EnrollmentDto>().Result;

            // Render the view with the selected enrollment details
            return View(selectedEnrollment);
        }

        // GET: Enrollment/Create
        // Displays the form for creating a new enrollment
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enrollment/Create
        // Handles submission of the form for creating a new enrollment
        [HttpPost]
        public ActionResult Create(EnrollmentDto enrollment)
        {
            string url = "addenrollment";
            HttpResponseMessage response = client.PostAsJsonAsync(url, enrollment).Result;
            if (response.IsSuccessStatusCode)
            {
                // Redirect to the list of enrollments after successful creation
                return RedirectToAction("List");
            }
            else
            {
                // Handle error (display error message or retry)
                return View(enrollment);
            }
        }

        // GET: Enrollment/Edit/1
        // Displays the form for editing a specific enrollment
        public ActionResult Edit(int id)
        {
            string url = "findenrollment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EnrollmentDto selectedEnrollment = response.Content.ReadAsAsync<EnrollmentDto>().Result;
            return View(selectedEnrollment);
        }

        // POST: Enrollment/Edit/1
        // Handles submission of the form for editing a specific enrollment
        [HttpPost]
        public ActionResult Edit(int id, EnrollmentDto enrollment)
        {
            string url = "updateenrollment/" + id;
            HttpResponseMessage response = client.PutAsJsonAsync(url, enrollment).Result;
            if (response.IsSuccessStatusCode)
            {
                // Redirect to the details of the updated enrollment
                return RedirectToAction("Details", new { id });
            }
            else
            {
                // Handle error (display error message or retry)
                return View(enrollment);
            }
        }

        // GET: Enrollment/Delete/1
        // Displays confirmation for deleting a specific enrollment
        public ActionResult Delete(int id)
        {
            string url = "findenrollment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EnrollmentDto selectedEnrollment = response.Content.ReadAsAsync<EnrollmentDto>().Result;
            return View(selectedEnrollment);
        }

        // POST: Enrollment/Delete/5
        // Handles deletion of a specific enrollment
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "deleteenrollment/" + id;
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                // Redirect to the list of enrollments after successful deletion
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
