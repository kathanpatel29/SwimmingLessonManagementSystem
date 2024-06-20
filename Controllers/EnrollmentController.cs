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
    /// <summary>
    /// Controller for handling views and actions related to enrollments.
    /// </summary>
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

        /// <summary>
        /// Displays a list of enrollments.
        /// </summary>
        /// <returns>ActionResult containing a view with a list of EnrollmentDto objects.</returns>
        /// <example>
        /// GET: Enrollment/List
        /// </example>
        public ActionResult List()
        {
            string url = "listenrollments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Deserialize response content to a list of EnrollmentDto objects
            IEnumerable<EnrollmentDto> enrollments = response.Content.ReadAsAsync<IEnumerable<EnrollmentDto>>().Result;

            // Render the view with the list of enrollments
            return View(enrollments);
        }

        /// <summary>
        /// Displays details of a specific enrollment.
        /// </summary>
        /// <param name="id">The ID of the enrollment to display.</param>
        /// <returns>ActionResult containing a view with details of the selected EnrollmentDto object.</returns>
        /// <example>
        /// GET: Enrollment/Details/1
        /// </example>
        public ActionResult Details(int id)
        {
            string url = "findenrollment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Deserialize response content to an EnrollmentDto object
            EnrollmentDto selectedEnrollment = response.Content.ReadAsAsync<EnrollmentDto>().Result;

            // Render the view with the selected enrollment details
            return View(selectedEnrollment);
        }

        /// <summary>
        /// Displays the form for creating a new enrollment.
        /// </summary>
        /// <returns>ActionResult containing a view for creating a new EnrollmentDto object.</returns>
        /// <example>
        /// GET: Enrollment/Create
        /// </example>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles submission of the form for creating a new enrollment.
        /// </summary>
        /// <param name="enrollment">The EnrollmentDto object containing data for the new enrollment.</param>
        /// <returns>ActionResult that redirects to the list of enrollments after successful creation, or re-displays the form on failure.</returns>
        /// <example>
        /// POST: Enrollment/Create
        /// </example>
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

        /// <summary>
        /// Displays the form for editing a specific enrollment.
        /// </summary>
        /// <param name="id">The ID of the enrollment to edit.</param>
        /// <returns>ActionResult containing a view for editing the selected EnrollmentDto object.</returns>
        /// <example>
        /// GET: Enrollment/Edit/1
        /// </example>
        public ActionResult Edit(int id)
        {
            string url = "findenrollment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EnrollmentDto selectedEnrollment = response.Content.ReadAsAsync<EnrollmentDto>().Result;
            return View(selectedEnrollment);
        }

        /// <summary>
        /// Handles submission of the form for editing a specific enrollment.
        /// </summary>
        /// <param name="id">The ID of the enrollment to edit.</param>
        /// <param name="enrollment">The updated EnrollmentDto object containing new data.</param>
        /// <returns>ActionResult that redirects to the details view of the updated enrollment after successful update, or re-displays the edit form on failure.</returns>
        /// <example>
        /// POST: Enrollment/Edit/1
        /// </example>
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

        /// <summary>
        /// Displays confirmation for deleting a specific enrollment.
        /// </summary>
        /// <param name="id">The ID of the enrollment to delete.</param>
        /// <returns>ActionResult containing a view for confirming deletion of the selected EnrollmentDto object.</returns>
        /// <example>
        /// GET: Enrollment/Delete/1
        /// </example>
        public ActionResult Delete(int id)
        {
            string url = "findenrollment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EnrollmentDto selectedEnrollment = response.Content.ReadAsAsync<EnrollmentDto>().Result;
            return View(selectedEnrollment);
        }

        /// <summary>
        /// Handles deletion of a specific enrollment.
        /// </summary>
        /// <param name="id">The ID of the enrollment to delete.</param>
        /// <param name="collection">Form collection (unused in this method).</param>
        /// <returns>ActionResult that redirects to the list of enrollments after successful deletion, or displays an error view on failure.</returns>
        /// <example>
        /// POST: Enrollment/Delete/5
        /// </example>
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
