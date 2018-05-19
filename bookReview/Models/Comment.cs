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
        public string UserID { get; set; }
        public int BookID { get; set; }
        public virtual Book Book { get; set; }
    }
}