using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookReview.Models
{
    public class Book
    {   [Key]
        public int BookID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(0,5)]
        public int Rating { get; set; }
        [Required]
        public string Description { get; set; }
        public int NumOfRaters { get; set; }
        public virtual ICollection<User> UsersWhoRated { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}