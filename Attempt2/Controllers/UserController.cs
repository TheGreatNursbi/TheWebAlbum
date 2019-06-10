using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attempt2.Models;
using Attempt2.ViewModels;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Net;

namespace Attempt2.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        ApplicationDbContext _context;

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        public UserController()
        {
            _context = new ApplicationDbContext();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAllImages(string searchString)
        {
            ViewBag.Liked = true;
            var media = _context.Images.Include(i => i.User)
                .Where(x => x.User.UserName.Contains(searchString) || searchString == null)
                .OrderByDescending(i => i.Id).ToList();
            return View(media);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserComment(int id)
        {
            var image = _context.Images
                .Include(u => u.User)
                .SingleOrDefault(i => i.Id == id);
            return View(image);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet]
        public FileResult UserDownload(string path)
        {
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }

    }
}