using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bookReview.Models;
namespace bookReview.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Comments(int? id)
        {
            using (_561EntityModel db = new _561EntityModel())
            {
                var c = db.Books.Where(u => u.BookID == id).FirstOrDefault();

                return View(c.Comment);
            }
        }
        public ActionResult DeleteC(int? id)
        {
            using (_561EntityModel db = new _561EntityModel())
            {
                var c = db.Comments.Where(u => u.CommentID == id).First();
                if (Session["Admin"] != null)
                {
                    bool f = Session["Admin"].Equals(true);
                    if (f)
                    {
                        db.Comments.Remove(c);
                        db.SaveChanges();
                        return RedirectToAction("BookList");
                    }
                }
                return RedirectToAction("NotAdmin");
            }
        }
        public ActionResult DeleteUser(int? id)
        {
            using (_561EntityModel db = new _561EntityModel())
            {
                var c = db.Users.Where(u => u.UserID == id).First();
                if (Session["Admin"] != null)
                {
                    bool f = Session["Admin"].Equals(true);
                    if (f)
                    {
                        db.Users.Remove(c);
                        db.SaveChanges();
                        return RedirectToAction("UserList");
                    }
                }
                return RedirectToAction("NotAdmin");
            }
        }


        [HttpPost]
        public ActionResult Comments(int id,string comment)
        {   
            using (_561EntityModel db = new _561EntityModel())
            {
                if (Session["UserID"] != null)
                {
                    var c = db.Books.Where(u => u.BookID == id).First();
                    Comment userComment = new Comment();
                    userComment.BookTitle = c.Title;
                    //userComment.Book = db.Books.Where(a => a.BookID == id).First();
                    userComment.CommentText = comment;
                    userComment.UserN = Session["Username"].ToString();
                    c.Comment.Add(userComment);
                    db.SaveChanges();
                    return View(c.Comment.ToList());
                }
                else return RedirectToAction("LogIn");
            }
        }
        
        public ActionResult NotAdmin()
        {
            return View();
        }
        public ActionResult Rating(int? id)
        {
            using (_561EntityModel db = new _561EntityModel())
            {
                var c = db.Books.Where(u => u.BookID == id).First();

                return View();
            }
        }
        [HttpPost]
        public ActionResult Rating(int id, Book r)
        {
            using (_561EntityModel db = new _561EntityModel())
            {
                if (Session["UserID"] != null)
                {
                    Book c = db.Books.Where(u => u.BookID == id).First();
                    int x = c.NumOfRaters+1;//because when anybody is creating a book he/she is not counted as a rater
                    string userID = Session["UserID"].ToString();
                    User user = db.Users.Where(u => u.UserID.ToString() == userID).First();
                    if (!c.UsersWhoRated.Contains(user))
                    {
                        c.NumOfRaters = x + 1;
                        c.Rating = (c.Rating + r.Rating) / c.NumOfRaters;
                        c.UsersWhoRated.Add(user);
                        db.SaveChanges();
                    }
                    return RedirectToAction("BookList");
                }
                else return RedirectToAction("LogIn");
            }
        }
        public ActionResult BookList()
        {   using (_561EntityModel db = new _561EntityModel())
            { 
                return View(db.Books.ToList());
            }
            
        }
        public ActionResult UserList()
        {
            using (_561EntityModel db = new _561EntityModel())
            {
                return View(db.Users.ToList());
            }

        }
        [HttpPost]
        public ActionResult BookList(string search)
        {
            using (_561EntityModel db = new _561EntityModel())
            {
                return View(db.Books.Where(a => a.Title.Contains(search) | a.Author.Contains(search)).ToList());
            }

        }
        public ActionResult CreateBook()
        {  
            ViewBag.Message = "Your create book page.";

            return View();
        }
        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                using (_561EntityModel db = new _561EntityModel())
            {
                  db.Books.Add(book);
                        db.SaveChanges();
                    }
                    ModelState.Clear();
                    ViewBag.Message = book.Title + "Saved";
                }

                return View();
         }
           
        public ActionResult Index(string search)
        {
           
                return RedirectToAction("BookList");
        }
        
        public ActionResult Register()
        {
            ViewBag.Message = "Your regirstry page.";

            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (_561EntityModel db = new _561EntityModel())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = user.UserName + "Success";
            }

            return RedirectToAction("LogIn");
        }
        public ActionResult LogIn()
        {
            ViewBag.Message = "Your login page.";

            return View();
        }
        [HttpPost]
        public ActionResult LogIn(User user)
        {
            using (_561EntityModel db = new _561EntityModel())
            {
                var usr = db.Users.Single(u => u.UserName == user.UserName && u.Password == user.Password);
                if(usr!= null)
                {
                    Session["Admin"] = usr.Admin;
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Username"] = usr.UserName.ToString();
                    ModelState.Clear();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is not correct");
                }
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session["Admin"] = null;
            Session["UserID"] = null;
            Session["Username"] = null;
            ModelState.Clear();
            return RedirectToAction("BookList");
        }
        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else return RedirectToAction("LogIn");
        }
    }
}