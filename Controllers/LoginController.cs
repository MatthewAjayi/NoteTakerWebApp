using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoteTakerWebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteTakerWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        //This constructor allows us to access the DB in this controller class and everywhere else in the code since public
        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                List<User> userList = _db.Users.ToList();

                //User user = new User();
                if (userList.Count() != 0)
                {
                    User loginUser = new User();
                    loginUser = userList.Where(a => a.Username.Equals(user.Username) && a.Password.Equals(user.Password)).FirstOrDefault();
                    if (loginUser != null)
                    {
                        UserStatic.UserName = loginUser.Username.ToString();
                        UserStatic.UserID = loginUser.Id;
                        TempData["UserID"] = loginUser.Id.ToString();
                        TempData["UserName"] = loginUser.Username.ToString();
                        return RedirectToAction("UserDashBoard");
                    }

                    //MessageBox.Show("No values in db  with that username or password");
                    else
                    {
                        TempData["Fail"] = "No users found in the db";
                        //return NotFound();
                    }

                }

            }

            return View();
        }

        public IActionResult UserDashBoard(int Id)
        {
            List<User> userList = _db.Users.ToList();
            User loginUser = new User();
            loginUser = userList.Where(a => a.Id.ToString() == (UserStatic.UserID.ToString())).FirstOrDefault();
            return View(loginUser);
        }

        public IActionResult EditUser(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var user = _db.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(User user)
        {
            return View();
        }

        public IActionResult Logout() 
        {
            UserStatic.UserName = "";
            UserStatic.UserID = 0;
            return RedirectToAction("Index", "Home"); 
        }    
    }


}

