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
    [Authorize(Roles = "user")]
    public class HomeController : Controller
    {
        ApplicationDbContext _context;
        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext
                .Current.User.Identity.GetUserId());

        /// <summary>
        /// 
        /// </summary>
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string searchString)
        {
            ViewBag.CurrentId = user.Id;
            ViewBag.Liked = true;
            var media = _context.Images.Include(i => i.User)
                .Where(x => x.User.UserName.Contains(searchString) || searchString == null)
                .OrderByDescending(i => i.Id).ToList();
            return View(media);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Id = user.Id;
            var model = new Image();

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetImagesOfUser(string searchString)
        {
            var images = _context.Images
                .Include(i => i.User)
                .Where(m => m.UserId == user.Id);

            var filteredImages = images
                .Where(x => x.User.UserName.Contains(searchString) || searchString == null)
                .OrderByDescending(x => x.Id).ToList();

            var userViewModel = new UserViewModel
            {
                AppUser = user, 
                Images = filteredImages
            };
            return View(userViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Image img)
        {
            if (ModelState.IsValid)
            {
                if (img.ImageFile != null && img.ImageFile.ContentLength > 0)
                {
                    img.NumberOfLikes = 0;
                    string fileName = Path.GetFileNameWithoutExtension(img.ImageFile.FileName);
                    string extension = Path.GetExtension(img.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    img.Path = "~/Content/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Image/"), fileName);
                    _context.Images.Add(img);
                    _context.SaveChanges();
                    RedirectToAction("Index");

                    img.ImageFile.SaveAs(fileName);
                }
                return RedirectToAction("GetImagesOfUser");
            }
            return View("Add");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string LikeThis(int id)
        {
            try
            {
                Image img = _context.Images.FirstOrDefault(x => x.Id == id);
                if (User.Identity.IsAuthenticated)
                {
                    img.NumberOfLikes+=1;
                    Like like = new Like();
                    like.ImageId = id;
                    like.UserId = user.Id;
                    like.LikedDate = DateTime.Now;
                    like.Liked = true;
                    _context.Likes.Add(like);
                    _context.SaveChanges();
                }
                return img.NumberOfLikes.ToString();
            }
            catch (Exception ex)
            {
                return string.Format(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UnLikeThis(int id)
        {
            Image img = _context.Images.FirstOrDefault(x => x.Id == id);
            if(User.Identity.IsAuthenticated)
            {
                Like like = _context
                    .Likes
                    .FirstOrDefault(x => x.ImageId == id && x.UserId == user.Id);
                img.NumberOfLikes-=1;
                _context.Likes.Remove(like);
                _context.SaveChanges();
            }
            return img.NumberOfLikes.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Comment(int id)
        {
            ViewBag.CurrentId = user.Id;
            ViewBag.Liked = true;
            var imageView = new ImageViewModel
            {
                Image = _context.Images.Include(u => u.User).SingleOrDefault(i => i.Id == id),
                AppUser = user
            };
            return View(imageView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public void DeleteFile(int? id)
        {
            if (id == null)
            {
                throw new Exception("Sorry, Id can not be NULL");
            }
            try
            {
                //Deleting the picture from database
                Image image = _context.Images.Find(id);
                if(image == null)
                {
                    throw new Exception("SOrry, Image is not found");
                }

                _context.Images.Remove(image);
                _context.SaveChanges();

                //Deleting a picture index from Like table
                var likes = _context.Likes
                    .Where(i => i.ImageId == id).ToList();
                _context.Likes.RemoveRange(likes);
                _context.SaveChanges();

                //Deleteing comments of this picture
                var comments = _context.Comments
                    .Where(i => i.ImageId == id).ToList();
                _context.Comments.RemoveRange(comments);
                _context.SaveChanges();


                var path = image.Path;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet]
        public FileResult Download(string path)
        {
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = _context.Images.SingleOrDefault(i => i.Id == id);
            if(image == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(image);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Image img)
        {
            if(!ModelState.IsValid)
            {

                return View("Edit", img);
            }

            if(img.Id == 0)
            {
                _context.Images.Add(img);
            }
            else
            {
                var imageInDb = _context.Images.Single(i => i.Id == img.Id);
                imageInDb.Title = img.Title;
                imageInDb.Summary = img.Summary;
                imageInDb.NumberOfLikes = img.NumberOfLikes;
                imageInDb.Path = img.Path;
                imageInDb.UserId = img.UserId;
            }
            _context.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

        public ActionResult Search(string searchString)
        {
            var images = _context.Images.Include(i => i.User).ToList();

            if(!String.IsNullOrWhiteSpace(searchString))
            {
                images = images.Where(s => s.User.UserName.Contains(searchString)).ToList();
            }
            return View(images);
        }
    }
}