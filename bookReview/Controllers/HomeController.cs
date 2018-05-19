using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                return View(db.Comments.Where(a => a.BookID == id).ToList());
            }
        }
        /*[HttpPost]
        public ActionResult Commentars2(string c,Book b)
        {   
            using (_561EntityModel db = new _561EntityModel())
            {
                Comment userComment = new Comment();
                userComment.BookID = b.BookID;
                userComment.Book = b;
                userComment.CommentText = c;
                userComment.UserID = Session["UserID"].ToString();
                return View(db.Comments.Where(a => a.BookID == b.BookID).ToList());
            }
        }*/
        public ActionResult BookList()
        {   using (_561EntityModel db = new _561EntityModel())
            { 
                return View(db.Books.ToList());
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
            using (_561EntityModel db = new _561EntityModel())
            {
                return View(db.Books.ToList());
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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

            return View();
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
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Username"] = usr.UserName.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is not correct");
                }
            }
            return View();
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