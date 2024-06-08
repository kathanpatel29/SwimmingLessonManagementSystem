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
    public class UserDataController : ApiController
    {
        // Database context for accessing the database
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UserData/ListUsers
        // Returns a list of all users in the system
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

        // GET: api/UserData/FindUser/id
        // Finds a user by ID and returns the user's details
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

        // POST: api/UserData/AddUser
        // Adds a new user to the system
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

        // PUT: api/UserData/UpdateUser/id
        // Updates an existing user's details
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

        // DELETE: api/UserData/DeleteUser/id
        // Deletes a user from the system
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

        // Helper method to check if a user exists by ID
        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}
