using Microsoft.AspNetCore.Mvc;
using NoteTakerWebApp.Models;

namespace NoteTakerWebApp.Controllers
{
    public class ViewNotesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ViewNotesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult ViewNotes()
        {
            IEnumerable<Notes> notes = _db.Notes.ToList();
            notes = _db.Notes.Where(userNotes => userNotes.UserId == UserStatic.UserID);
            return View(notes);
        }

        public IActionResult DeleteNote(int? id)
        {
            try
            {
                if (id == null)
                {

                    return NotFound();
                }

                var note = _db.Notes.FirstOrDefault(x => x.Id == id);

                if (note == null)
                {

                    return NotFound();
                }

                return View(note);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteListNote(int? id)
        {
            try
            {
                var note = _db.Notes.FirstOrDefault(x => x.Id == id);
                if (note == null)
                {

                    return NotFound();
                }

                _db.Notes.Remove(note);
                _db.SaveChanges();
                return RedirectToAction("UserDashBoard", "Login");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
