using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attempt2.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public DateTime CommentedDate{ get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string UserName{ get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser AppUser { get; set; }

        [Required]
        public int ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }

    }
}