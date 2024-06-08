using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using SwimmingLessonManagementSystem.Models;
using Newtonsoft.Json;

namespace SwimmingLessonManagementSystem.Controllers
{
    public class UserController : Controller
    {
        // Static HttpClient instance for making API calls
        private static readonly HttpClient client;

        // Static constructor to initialize the HttpClient
        static UserController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44358/api/userdata/");
        }

        // GET: User/List
        // Retrieves the list of users and displays it
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

        // GET: User/Details/1
        // Retrieves the details of a specific user by ID and displays it
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

        // GET: User/Create
        // Displays the form for creating a new user
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // Submits the data for creating a new user
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

        // GET: User/Edit/id
        // Displays the form for editing an existing user
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

        // POST: User/Edit/1
        // Submits the data for editing an existing user
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

        // GET: User/Delete/1
        // Displays the form for deleting a user
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

        // POST: User/Delete/1
        // Submits the request to delete a user
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
