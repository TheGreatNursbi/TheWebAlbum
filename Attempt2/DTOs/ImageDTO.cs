using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attempt2.DTOs
{
    public class ImageDTO
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Path of the Picture")]
        public string Path { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public string UserId { get; set; }
    }
}