namespace bookReview
{
    using bookReview.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class _561EntityModel : DbContext
    {

        public _561EntityModel()
            : base("name=561EntityModelFinal")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }

}