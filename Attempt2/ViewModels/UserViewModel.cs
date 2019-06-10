using Attempt2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attempt2.ViewModels
{
    public class UserViewModel
    {
        public ApplicationUser AppUser { get; set; }
        public List<Image> Images { get; set; }
    }
}