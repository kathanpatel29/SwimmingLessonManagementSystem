using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using SwimmingLessonManagementSystem.Models;
using Newtonsoft.Json;

namespace SwimmingLessonManagementSystem.Controllers
{
    /// <summary>
    /// MVC Controller for managing user interactions.
    /// </summary>
    public class UserController : Controller
    {
        // Static HttpClient instance for making API calls
        private static readonly HttpClient client;

        /// <summary>
        /// Static constructor to initialize the HttpClient.
        /// </summary>
        static UserController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44358/api/userdata/");
        }

        /// <summary>
        /// Retrieves the list of users and displays it.
        /// </summary>
        /// <returns>An ActionResult that renders the list of users.</returns>
        /// <example>
        /// GET: User/List
        /// </example>
        public async Task<ActionResult> List()
        {
            string url = "listusers";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonString);
                return View(users);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }

        /// <summary>
        /// Retrieves the details of a specific user by ID and displays it.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An ActionResult that renders the user's details.</returns>
        /// <example>
        /// GET: User/Details/1
        /// </example>
        public async Task<ActionResult> Details(int id)
        {
            string url = "finduser/" + id;
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var selectedUser = JsonConvert.DeserializeObject<UserDto>(jsonString);
                return View(selectedUser);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }

        /// <summary>
        /// Displays the form for creating a new user.
        /// </summary>
        /// <returns>An ActionResult that renders the create user form.</returns>
        /// <example>
        /// GET: User/Create
        /// </example>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Submits the data for creating a new user.
        /// </summary>
        /// <param name="user">The UserDto object containing the user data.</param>
        /// <returns>An ActionResult that redirects to the list of users if successful.</returns>
        /// <example>
        /// POST: User/Create
        /// </example>
        [HttpPost]
        public async Task<ActionResult> Create(UserDto user)
        {
            string url = "adduser";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, user);

            if (response.IsSuccessStatusCode)
            {
                // Redirect to list after successful creation
                return RedirectToAction("List");
            }
            else
            {
                // Handle error
                return View(user);
            }
        }

        /// <summary>
        /// Displays the form for editing an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <returns>An ActionResult that renders the edit user form.</returns>
        /// <example>
        /// GET: User/Edit/1
        /// </example>
        public async Task<ActionResult> Edit(int id)
        {
            string url = "finduser/" + id;
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var selectedUser = JsonConvert.DeserializeObject<UserDto>(jsonString);
                return View(selectedUser);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }

        /// <summary>
        /// Submits the data for editing an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <param name="user">The UserDto object containing the updated user data.</param>
        /// <returns>An ActionResult that redirects to the user details if successful.</returns>
        /// <example>
        /// POST: User/Edit/1
        /// </example>
        [HttpPost]
        public async Task<ActionResult> Edit(int id, UserDto user)
        {
            string url = "updateuser/" + id;
            HttpResponseMessage response = await client.PutAsJsonAsync(url, user);

            if (response.IsSuccessStatusCode)
            {
                // Redirect to details after successful update
                return RedirectToAction("Details", new { id });
            }
            else
            {
                // Handle error
                return View(user);
            }
        }

        /// <summary>
        /// Displays the form for deleting a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An ActionResult that renders the delete user form.</returns>
        /// <example>
        /// GET: User/Delete/1
        /// </example>
        public async Task<ActionResult> Delete(int id)
        {
            string url = "finduser/" + id;
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var selectedUser = JsonConvert.DeserializeObject<UserDto>(jsonString);
                return View(selectedUser);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }

        /// <summary>
        /// Submits the request to delete a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <param name="collection">Form collection (not used).</param>
        /// <returns>An ActionResult that redirects to the list of users if successful.</returns>
        /// <example>
        /// POST: User/Delete/1
        /// </example>
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            string url = "deleteuser/" + id;
            HttpResponseMessage response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Redirect to list after successful deletion
                return RedirectToAction("List");
            }
            else
            {
                // Handle error
                return View();
            }
        }
    }
}
