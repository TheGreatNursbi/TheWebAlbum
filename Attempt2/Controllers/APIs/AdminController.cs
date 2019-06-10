using Attempt2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using Attempt2.DTOs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Attempt2.Controllers.APIs
{
    public class AdminController : ApiController
    {
        UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _context;

        public AdminController()
        {
            userManager = new UserManager<ApplicationUser>
                (new UserStore<ApplicationUser>(new ApplicationDbContext()));

            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// /api/admin/
        /// </summary>
        /// <returns>Gets all Users</returns>
        [HttpGet()]
        public IHttpActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            if (users.Count > 0)
                return Ok(users.Select(Mapper.Map<ApplicationUser, ApplicationUserDTO>));
            else
                return NotFound();
        }

        /// <summary>
        /// /api/admin/{id}
        /// Gets Customer by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return ApplicationUser object</returns>
        [HttpGet()]
        public async Task<IHttpActionResult> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(Mapper.Map<ApplicationUser, ApplicationUserDTO>(user));
        }

        [HttpDelete()]
        public void Delete(string id)
        {
            var user = userManager.FindById(id);
            userManager.DeleteAsync(user);

            _context.Images.RemoveRange(_context.Images.Where(i => i.UserId == id));
            _context.Comments.RemoveRange(_context.Comments.Where(i => i.UserId == id));
            _context.Likes.RemoveRange(_context.Likes.Where(i => i.UserId == id));
            _context.SaveChanges();
        }

    }
}
