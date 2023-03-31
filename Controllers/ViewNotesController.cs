using Microsoft.AspNetCore.Mvc;
using NoteTakerWebApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NoteTakerWebApp.Controllers
{
    public class ViewNotesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ViewNotesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var notes = _db.Notes.Where(userNotes => userNotes.UserId == UserStatic.UserID).ToList();
            //notes = _db.Notes.Where(userNotes => userNotes.UserId == UserStatic.UserID);
            ViewBag.Notes = notes;
            return View(notes);
        }

        [HttpPost]
        public IActionResult Delete(int?id)
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

                //return PartialView("_DeletePartialView", note);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //POST
        public ActionResult EditNotes(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var note = _db.Notes.Find(id);
            //NotesStatic.Title = note.Name.ToString();
            //NotesStatic.ID = note.Id;
            //NotesStatic.Description = note.Description;

            if (note == null)
            {
                return NotFound();
            }

            //return PartialView("_EditModalPartial");
            return View(note);
        }

        //POST
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult EditNotes(Notes note)
        {
            try
            {
                //Notes updatedNotes = _db.Notes.FirstOrDefault(x => x.Id == id);
                //note.UserId = UserStatic.UserID;    
                if (note == null) { return NotFound(); }

                if (ModelState.IsValid)
                {
                    _db.Notes.Update(note);
                    _db.SaveChanges();
                    //return RedirectToAction("Index");
                    //return NotFound();
                }

                //return PartialView("_EditModalPartial", note);

                return RedirectToAction("Index");

                //return View(note);      

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
