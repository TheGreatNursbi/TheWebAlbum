using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Attempt2.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace Attempt2
{
    [Authorize]
    public class CommentHub : Hub
    {
        ApplicationDbContext _context;

        public CommentHub()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        public void Send(string userId, string summary, int imageId, string userName)
        {
            var date = DateTime.Now;
            var comment = new Comment
            {
                CommentedDate = date,
                Summary = summary,
                UserId = userId,
                ImageId = imageId, 
                UserName = userName
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();

            Clients.All.addNewCommentToPage(userName, summary, date.ToString());
        }
    }
}