using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Attempt2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attempt2.ViewModels
{
    public class ImageViewModel
    {
        public ApplicationUser AppUser { get; set; }
        public Image Image { get; set; }
    }
}