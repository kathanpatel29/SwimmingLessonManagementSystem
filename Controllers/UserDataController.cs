using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SwimmingLessonManagementSystem.Models;

namespace SwimmingLessonManagementSystem.Controllers
{
    /// <summary>
    /// API Controller for managing user data.
    /// </summary>
    public class UserDataController : ApiController
    {
        // Database context for accessing the database
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all users in the system.
        /// </summary>
        /// <returns>A list of UserDto objects representing all users.</returns>
        /// <example>
        /// GET: api/UserData/ListUsers
        /// </example>
        [HttpGet]
        [Route("api/UserData/ListUsers")]
        public IEnumerable<UserDto> ListUsers()
        {
            List<User> users = db.Users.ToList();
            List<UserDto> userDtos = new List<UserDto>();

            users.ForEach(u => userDtos.Add(new UserDto()
            {
                UserID = u.UserID,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role
            }));

            return userDtos;
        }

        /// <summary>
        /// Finds a user by ID and returns the user's details.
        /// </summary>
        /// <param name="id">The ID of the user to find.</param>
        /// <returns>An IHttpActionResult containing the UserDto object if found, otherwise NotFound.</returns>
        /// <example>
        /// GET: api/UserData/FindUser/5
        /// </example>
        [ResponseType(typeof(UserDto))]
        [HttpGet]
        [Route("api/UserData/FindUser/{id}")]
        public IHttpActionResult FindUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            UserDto userDto = new UserDto()
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };

            return Ok(userDto);
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="userDto">The UserDto object representing the new user.</param>
        /// <returns>An IHttpActionResult containing the created UserDto object.</returns>
        /// <example>
        /// POST: api/UserData/AddUser
        /// </example>
        [ResponseType(typeof(UserDto))]
        [HttpPost]
        [Route("api/UserData/AddUser")]
        public IHttpActionResult AddUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User()
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Role = userDto.Role
            };

            db.Users.Add(user);
            db.SaveChanges();

            userDto.UserID = user.UserID;

            return CreatedAtRoute("DefaultApi", new { id = user.UserID }, userDto);
        }

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userDto">The UserDto object containing the updated details.</param>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
        /// <example>
        /// PUT: api/UserData/UpdateUser/5
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/UserData/UpdateUser/{id}")]
        public IHttpActionResult UpdateUser(int id, UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDto.UserID)
            {
                return BadRequest();
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Role = userDto.Role;

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes a user from the system.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An IHttpActionResult containing the deleted User object if successful, otherwise NotFound.</returns>
        /// <example>
        /// DELETE: api/UserData/DeleteUser/5
        /// </example>
        [ResponseType(typeof(User))]
        [HttpDelete]
        [Route("api/UserData/DeleteUser/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        /// <summary>
        /// Checks if a user exists by ID.
        /// </summary>
        /// <param name="id">The ID of the user to check.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}
