using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookReview.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}