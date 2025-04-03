using LibraryManagement.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.BooksProcessor;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {


        // GET: Books
        public ActionResult Index()
        {
            ViewBag.Message = "Viewbooks";
             
            var data = LoadBooks();
            List<BooksModels> books = new List<BooksModels>();

            foreach (var row in data)
            {
                books.Add(new BooksModels
                {
                   Id = row.Id,
                    BookId = row.BookId,
                    BookName = row.BookName,
                    BookAuthor = row.BookAuthor,
                    BookDescription = row.BookDescription,
                    BookStock = row.BookStock,
                    Genre = row.Genre,
                    Language = row.Language,
                    Cost = row.Cost,
                    Edition = row.Edition,
                    BookImage = row.BookImage
                });
            }

            return View(books);
        }
        
        [Authorize(Roles = "Admin")]

        public ActionResult AddBook()
        {
            ViewBag.Message = "Add Books Here";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(BooksModels model)
        {
            ViewBag.Message = "Add Books Here";

            if (ModelState.IsValid)
            {

                int recordsCreated = CreateBook(model.BookId, model.BookName, model.BookAuthor, model.BookDescription, model.BookStock, model.Genre, model.Language, model.Cost, model.Edition, model.BookImage);
                return RedirectToAction("Index");


            }

            return View();
        }
        public ActionResult EditBooks(int id, BooksModels model)
        {
            ViewBag.Message = "Edit Books Here";
            var detail = LoadOneBooks(id);

            DataLibrary.Models.BooksModels books = new DataLibrary.Models.BooksModels();

            foreach (var row in detail)
            {
                
                var boks = new DataLibrary.Models.BooksModels
                {
                    Id = row.Id,
                    BookId = row.BookId,
                    BookName = row.BookName,
                    BookAuthor = row.BookAuthor,
                    BookDescription = row.BookDescription,
                    BookStock = row.BookStock,
                    Genre = row.Genre,
                    Language = row.Language,
                    Cost = row.Cost,
                    Edition = row.Edition,
                    BookImage = row.BookImage
                };
                
                books = boks;
            }

            if (ModelState.IsValid)
            {
                var recordsEdited = EditBook(model.Id, model.BookId, model.BookName, model.BookAuthor, model.BookDescription, model.BookStock, model.Genre, model.Language, model.Cost, model.Edition, model.BookImage);

            }
            return View("EditBooks", books);
        }

        public ActionResult BookDetails(int id)
        {

            var detail = LoadOneBooks(id);

            BooksModels books = new BooksModels();

            foreach (var row in detail)
            {
                var bok = new BooksModels
                {
                    Id = row.Id,
                    BookId = row.BookId,
                    BookName = row.BookName,
                    BookAuthor = row.BookAuthor,
                    BookDescription = row.BookDescription,
                    BookStock = row.BookStock,
                    Genre = row.Genre,
                    Language = row.Language,
                    Cost = row.Cost,
                    Edition = row.Edition,
                    BookImage = row.BookImage
                };
                books = bok;
            }

            return View("BookDetails", books);
        }
        public ActionResult DeleteBooks(int id)
        {
            int deletedBooks = DeleteBook(id);
            List<BooksModels> books = new List<BooksModels>();
            return RedirectToAction("Index", books);
        }
    }
}