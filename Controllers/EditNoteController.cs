using Microsoft.AspNetCore.Mvc;
using NoteTakerWebApp.Models;

namespace NoteTakerWebApp.Controllers
{
    public class EditNoteController : Controller
    {
        private readonly ApplicationDbContext _db;
        public EditNoteController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult EditNotes(int? id)
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
        public IActionResult EditCurrentNotes(int? id)
        {
            try
            {
                var note = _db.Notes.FirstOrDefault(x => x.Id == id);
                if (note == null)
                {
                    return NotFound();
                }

                _db.Notes.Update(note);
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
