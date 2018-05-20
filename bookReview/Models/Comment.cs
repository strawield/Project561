using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookReview.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public string UserN { get; set; }
        public string BookTitle { get; set; }
        //public virtual Book Book { get; set; }
    }
}