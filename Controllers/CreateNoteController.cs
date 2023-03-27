using Microsoft.AspNetCore.Mvc;
using NoteTakerWebApp.Models;

namespace NoteTakerWebApp.Controllers
{
    public class CreateNoteController : Controller
    {
        private readonly ApplicationDbContext _db;

        //This constructor allows us to access the DB in this controller class and everywhere else in the code since public
        public CreateNoteController(ApplicationDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Index()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewNote(Notes note)
        {
            if (ModelState.IsValid)
            {
                note.UserId = UserStatic.UserID;
                _db.Notes.Add(note);
                _db.SaveChanges();
                TempData["sucess"] = "Note was created successfully!";
                return RedirectToAction("Index", "ViewNotes");
            }

            else
            {
                TempData["Fail"] = "Note was note created please try again!";
                return View("Index");
            }
   
        }
        
    }
}
