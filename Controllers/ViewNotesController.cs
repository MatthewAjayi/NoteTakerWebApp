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
        public IActionResult Index()
        {
            IEnumerable<Notes> notes = _db.Notes.ToList();
            notes = _db.Notes.Where(userNotes => userNotes.UserId == UserStatic.UserID);
            return View(notes);
        }

        //public IActionResult Delete(int?id)
        //{
        //    try
        //    {

        //        var note = _db.Notes.FirstOrDefault(x => x.Id == id);

        //        if (note == null)
        //        {
        //            return NotFound();
        //        }

        //        return PartialView("_DeletePartialView", note);

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

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

        //public IActionResult EditNotes(int? id)
        //{
        //    try
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var note = _db.Notes.FirstOrDefault(x => x.Id == id);

        //        if (note == null)
        //        {
        //            return NotFound();
        //        }

        //        return RedirectToAction("EditNotes", note);

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

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
