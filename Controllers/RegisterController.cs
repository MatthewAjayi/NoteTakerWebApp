using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoteTakerWebApp.Models;

namespace NoteTakerWebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _db;

        //This constructor allows us to access the DB in this controller class and everywhere else in the code since public
        public RegisterController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Lists all users in the DB
        public IActionResult Index()
        {
            //var objUserList = _db.Users.ToList();
            IEnumerable<User> users = _db.Users.ToList();
            return View(users);
        }

        //GET
        public IActionResult CreateUser()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(User user)
        {
            //Use ModelState.AddModelError to add error on the html side of things
            if(ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                TempData["sucess"] = user.Username + " was created successfully!";
                UserStatic.UserName = user.Username.ToString();
                UserStatic.UserID = user.Id;
                return RedirectToAction("Index", "ViewNotes");
                //return RedirectToAction("Index");
                //return RedirectToAction("Index", "Home");
            }

            return View(user); 
        }
    }
}